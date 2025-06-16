using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IMapper mapper;

        // private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(IMapper mapper, IRegionRepository regionRepository)
        {
            this.mapper = mapper;
            // this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regionsDomain = await regionRepository.GetAllAsync();
            // map domain models to Dtos
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var region = await regionRepository.GetById(id); // used for ids only
            
            if (region == null) { 
            return NotFound();
            }
            
            return Ok(mapper.Map<RegionDto>(region));
        }

        // Post To Create New Region
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Map or Convert DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(region);
            //var regionDomainModel = new Region { 
               
            //    Name = region.Name , 
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl};

            // Save Data to DataBase
            regionDomainModel = await regionRepository.CreateRegionAsync(regionDomainModel);


            // Map DomainModel to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            
            // var regionDto = new RegionDto { Id = regionDomainModel.Id, Name = region.Name ,Code = regionDomainModel.Code, RegionImageUrl = regionDomainModel.RegionImageUrl };


            return CreatedAtAction(nameof(GetById),new {id=regionDomainModel.Id},regionDto );

        }


        // Update Region
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute]Guid id ,[FromBody] UpdateRegionRequestDto region)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Map  DTO to Domain Model

            var regionDomainModel = mapper.Map<Region>(region);
            //var regionDomainModel = new Region
            //{
            //    Code = region.Code ,
            //    RegionImageUrl = region.RegionImageUrl ,
            //    Name = region.Name ,
            //};

            regionDomainModel = await regionRepository.UpdateRegionAsync(id, regionDomainModel);
            if (regionDomainModel == null) {
            return NotFound();
            }

            // Map DomainModel to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            
            //var regionDto = new RegionDto { 
            //    Id = regionDomainModel.Id, 
            //    Name = region.Name, 
            //    Code = regionDomainModel.Code, 
            //    RegionImageUrl = regionDomainModel.RegionImageUrl };


            return Ok(regionDto);

        }


        // Delete Region
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {

            var region = await regionRepository.DeleteRegionAsync(id);
            if (region == null) {
            return NotFound("Region Not Found");
            }
            var regionDto = mapper.Map<RegionDto>(region);
            //var regionDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};

            return Ok(regionDto);

        }


    }
}
