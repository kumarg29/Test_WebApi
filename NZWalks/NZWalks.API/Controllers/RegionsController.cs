using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext DbContext;
        private readonly IRegionRepository _regionrepository;
        private readonly IMapper _mapper;

        public RegionsController(NZWalksDBContext dbcontext, IRegionRepository regionrepository, IMapper mapper)
        {
            DbContext = dbcontext;
            this._regionrepository = regionrepository;
            this._mapper = mapper;
        }


        //GET : https://lpcalhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from database - Domain models
            var RegionDomain = await _regionrepository.GetAllAsync();
            //Map Domain Model to DTOs
            //var regionDto = new List<RegionDto>();
            //foreach (var region in RegionDomain)
            //{
            //    regionDto.Add(new RegionDto
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        ImageUrl = region.ImageUrl

            //    });
            //}

            // return DTOs back to the client
            return Ok(_mapper.Map<List<RegionDto>>(RegionDomain));
        }

        //GET : https://localhost:portnumber/api/regions/{Id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = DbContext.Regions.Find(Id);
            // Get Region Domain Model from Database
            var RegionDomainModel = await _regionrepository.GetByIdAsync(id);


            if (RegionDomainModel == null)
            {
                return NotFound();
            }

            //Map Region Domain Model to Region DTO
            //var RegionDto = new RegionDto
            //{
            //    Id = RegionDomainModel.Id,
            //    Code = RegionDomainModel.Code,
            //    Name = RegionDomainModel.Name,
            //    ImageUrl = RegionDomainModel.ImageUrl
            //};
            //Return DTO Back To Client
            return Ok(_mapper.Map<RegionDto>(RegionDomainModel));
        }

        //POST : https://localhost:portnumber/api/regions
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addregiondto)
        {
            // Map Dto to Domain Model
            //var RegionDomainModel = new Region
            //{
            //    Code = request.Code,
            //    Name = request.Name,
            //    ImageUrl = request.ImageUrl
            //};
            var RegionDomainModel = _mapper.Map<Region>(addregiondto);

            //Use Domain Model to create Region
            await DbContext.Regions.AddAsync(RegionDomainModel);
            DbContext.SaveChanges();

            // Map Domain Model Back To DTO
            //var RegionDto = new RegionDto
            //{
            //    Id = RegionDomainModel.Id,
            //    Code = RegionDomainModel.Code,
            //    Name = RegionDomainModel.Name,
            //    ImageUrl = RegionDomainModel.ImageUrl
            //};
            var RegionDto = _mapper.Map<RegionDto>(RegionDomainModel);
            return CreatedAtAction(nameof(GetById), new { Id = RegionDto.Id }, RegionDto);
        }

        //Update Region
        //PUT : https//localhost:postnumber/api/regions/{id}
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid Id, [FromBody] UpdateRegionRequestDto updateregionrequestdto)
        {
            // Map Dto to Domain Model
            //var RegionDomainModel = new Region
            //{
            //    Code = updateregionrequestdto.Code,
            //    Name = updateregionrequestdto.Name,
            //    ImageUrl = updateregionrequestdto.ImageUrl
            //};

            var RegionDomainModel = _mapper.Map<Region>(updateregionrequestdto);
            //Check if the region exists
            RegionDomainModel = await _regionrepository.UpdateRegionAsync(Id, RegionDomainModel);

            if (RegionDomainModel == null)
            {
                return NotFound();
            }

            //Convert Domain Model to Dto
            //var RegionDto = new RegionDto
            //{
            //    Id = RegionDomainModel.Id,
            //    Code = RegionDomainModel.Code,
            //    Name = RegionDomainModel.Name,
            //    ImageUrl = RegionDomainModel.ImageUrl
            //};
            var RegionDto = _mapper.Map<UpdateRegionRequestDto>(RegionDomainModel);

            return Ok(RegionDto);

        }


        //Delete Region
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid Id)
        {
            //var RegionDomainModel = await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            var RegionDomainModel = await _regionrepository.DeleteRegionAsync(Id);
            if (RegionDomainModel == null)
            {
                return NotFound();
            }


            //Return Delete Region Back
            //Map Domain Model to Dto
            //var RegionDto = new RegionDto
            //{
            //    Id = RegionDomainModel.Id,
            //    Code = RegionDomainModel.Code,
            //    Name = RegionDomainModel.Name,
            //    ImageUrl = RegionDomainModel.ImageUrl
            //};
            var RegionDto = _mapper.Map<RegionDto>(RegionDomainModel);

            return Ok(RegionDto);
        }


    }
}
