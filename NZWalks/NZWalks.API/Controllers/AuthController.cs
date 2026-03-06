using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _Usermanager;
        private readonly ITokenRepository _Tokenrepository;

        public AuthController(UserManager<IdentityUser> Usermanager, ITokenRepository TokenRepository)
        {
            _Usermanager = Usermanager;
            _Tokenrepository = TokenRepository;
        }
        //POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerrequestdto)
        {
            var IdentityUser = new IdentityUser
            {
                UserName = registerrequestdto.Username,
                Email = registerrequestdto.Username
            };
            var IdentityResult = await _Usermanager.CreateAsync(IdentityUser, registerrequestdto.Password);

            if (IdentityResult.Succeeded)
            {
                //Add Roles To this User
                if (registerrequestdto.Roles != null && registerrequestdto.Roles.Any())
                {
                    IdentityResult = await _Usermanager.AddToRolesAsync(IdentityUser, registerrequestdto.Roles);
                    if (IdentityResult.Succeeded)
                    {
                        return Ok("User Was Created Successfully");
                    }
                }

            }
            return BadRequest("Something Went Wrong");

        }

        //POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto Loginrequestdto)
        {
            var User = await _Usermanager.FindByEmailAsync(Loginrequestdto.UserName);
            if (User != null)
            {
                var CheckPasswordResult = await _Usermanager.CheckPasswordAsync(User, Loginrequestdto.Password);
                if (CheckPasswordResult)
                {
                    //Get Roles For This User
                    var roles = await _Usermanager.GetRolesAsync(User);
                    if (roles != null)
                    {
                        // Create Token
                        var JwtToken = _Tokenrepository.CreateJwtToken(User, roles.ToList());
                        // Convert It To Dto
                        var Response = new LoginResponseDto
                        {
                            JwtToken = JwtToken
                        };
                        return Ok(Response);
                    }
                }
            }
            return BadRequest("UserName Or Password Incorrect");
        }

    }
}
