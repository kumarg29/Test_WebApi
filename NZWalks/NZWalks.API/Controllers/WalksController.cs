using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _repository;

        public WalksController(IMapper mapper, IWalkRepository repository)
        {
            this._mapper = mapper;
            this._repository = repository;
        }
        // POST: /Api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalks([FromBody] AddWalksRequestDto addWalksRequestDto)
        {

            //Convert Dto to Domain Model
            var WalkDomainModel = _mapper.Map<Walk>(addWalksRequestDto);
            await _repository.CreateWalkAsync(WalkDomainModel);
            //Map Domain Model to 
            return Ok(_mapper.Map<WalkDto>(WalkDomainModel));

        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var WalksDomainModel = await _repository.GetAllAsync();
            //Map Domain Model To Dto
            return Ok(_mapper.Map<List<WalkDto>>(WalksDomainModel));
        }

        // Get Walk by Id
        [HttpGet]
        // GET: /api/walks/{Id}
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)  // We will get the Id from the route that's why we use from route
        {
            var Walkdomainmodel = await _repository.GetByIdAsync(Id);
            if (Walkdomainmodel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDto>(Walkdomainmodel));

        }
        // Update Walk by Id
        //GET: /api/Walks/{Id}
        [HttpPut]
        [Route("{Id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid Id, UpdateWalkRequestDto updateWalkRequestDto)
        {

            // Map Dto to Domain Model
            var WalkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);

            WalkDomainModel = await _repository.UpdateAsync(Id, WalkDomainModel);

            if (WalkDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model To Dto

            return Ok(_mapper.Map<WalkDto>(WalkDomainModel));


        }

        // Delete Walk by Id
        // Delete: /api/Walk/{Id}
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid Id)
        {
            var WalkDomainModel = await _repository.DeleteWalkAsync(Id);
            if (WalkDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model To Dto
            var walkdto = _mapper.Map<WalkDto>(WalkDomainModel);
            return Ok(walkdto);

        }
    }
}

