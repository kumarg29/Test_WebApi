using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext DbContext;
        public RegionsController(NZWalksDBContext dbcontext)
        {
            DbContext = dbcontext;
        }


        //GET : https://lpcalhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            //Get Data from database - Domain models
            var RegionDomain = DbContext.Regions.ToList();
            //Map Domain Model to DTOs
            var regionDto = new List<RegionDto>();
            foreach (var region in RegionDomain)
            {
                regionDto.Add(new RegionDto
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    ImageUrl = region.ImageUrl

                });
            }
            // return DTOs back to the client
            return Ok(regionDto);
        }

        //GET : https://localhost:portnumber/api/regions/{Id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region = DbContext.Regions.Find(Id);
            // Get Region Domain Model from Database
            var RegionDomainModel = DbContext.Regions.FirstOrDefault(x => x.Id == id);


            if (RegionDomainModel == null)
            {
                return NotFound();
            }

            //Map Region Domain Model to Region DTO
            var RegionDto = new RegionDto
            {
                Id = RegionDomainModel.Id,
                Code = RegionDomainModel.Code,
                Name = RegionDomainModel.Name,
                ImageUrl = RegionDomainModel.ImageUrl
            };
            //Return DTO Back To Client
            return Ok(RegionDto);
        }

        //POST : https://localhost:portnumber/api/regions
        [HttpPost]
        public IActionResult CreateRegion([FromBody] AddRegionRequestDto request)
        {
            // Map Dto to Domain Model
            var RegionDomainModel = new Region
            {
                Code = request.Code,
                Name = request.Name,
                ImageUrl = request.ImageUrl
            };

            //Use Domain Model to create Region
            DbContext.Regions.Add(RegionDomainModel);
            DbContext.SaveChanges();

            // Map Domain Model Back To DTO
            var RegionDto = new RegionDto
            {
                Id = RegionDomainModel.Id,
                Code = RegionDomainModel.Code,
                Name = RegionDomainModel.Name,
                ImageUrl = RegionDomainModel.ImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { Id = RegionDto.Id }, RegionDto);
        }

        //Update Region
        //PUT : https//localhost:postnumber/api/regions/{id}
        [HttpPut]
        [Route("{Id:Guid}")]
        public IActionResult UpdateRegion([FromRoute] Guid Id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Check if the region exists
          var regiondomainmodel =  DbContext.Regions.FirstOrDefault(x => x.Id == Id);

            if (regiondomainmodel == null)
            {
                return NotFound();
            }

            // Map Dto to domain model
            regiondomainmodel.Code = updateRegionRequestDto.Code;
            regiondomainmodel.Name = updateRegionRequestDto.Name;
            regiondomainmodel.ImageUrl = updateRegionRequestDto?.ImageUrl;

            DbContext.SaveChanges();

            //Convert Domain Model to Dto
            var RegionDto = new RegionDto
            {
                Id = regiondomainmodel.Id,
                Code = regiondomainmodel.Code,
                Name = regiondomainmodel.Name,
                ImageUrl = regiondomainmodel.ImageUrl
            };

            return Ok(RegionDto);

        }

        //Delete Region
        [HttpDelete]
        [Route("{Id:Guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid Id )
        {
            var RegionDomainModel = DbContext.Regions.FirstOrDefault(x => x.Id == Id);
            if(RegionDomainModel== null)
            {
                return NotFound();
            }
            //delete region

            DbContext.Regions.Remove(RegionDomainModel);
            DbContext.SaveChanges();

            //Return Delete Region Back
            //Map Domain Model to Dto
            var RegionDto = new RegionDto
            {
                Id = RegionDomainModel.Id,
                Code = RegionDomainModel.Code,
                Name = RegionDomainModel.Name,
                ImageUrl = RegionDomainModel.ImageUrl
            };

            return Ok(RegionDto);
        }


    }
}
