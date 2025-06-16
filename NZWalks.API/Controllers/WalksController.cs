using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // GET:
        // api/<WalksController>?FilterOn=name&filterQuery=Track
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, 
            [FromQuery] string? filterQuery, 
            [FromQuery] string? SortOn, 
            [FromQuery] bool? isAscending,
             [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
           
            
            )
        {
            var walkDomainModel = await walkRepository.GetAllWalkAsync(filterOn, filterQuery,
                                                                        SortOn,
                                                                        isAscending ?? true, 
                                                                        pageNumber,
                                                                        pageSize);
            var walkDtos = mapper.Map<List<WalkDto>>(walkDomainModel);
            return Ok(walkDtos);
        }

        // GET api/<WalksController>/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var walkDomainModel = await walkRepository.GetWalkAsync(id);
            var walkDtos = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDtos);
        }
        

        // Create Walk
        // POST api/<WalksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddWalkRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Map Dto to Walk
            var walkDomainModel = mapper.Map<Walk>(requestDto);
            var updatedWalk = await walkRepository.createWalkAsync(walkDomainModel);
            var walkDto = mapper.Map<WalkDto>(updatedWalk);
            return Ok(walkDto);
        }

        // PUT api/<WalksController>/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute]Guid id, [FromBody] UpdateWalkRequestDto walkRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var walkDomainModel = await walkRepository.GetWalkAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound("This walk doesn't exist");
            }

            Console.WriteLine("---------------------Walk Name-------------------------");
            Console.WriteLine(walkDomainModel.Name);
            walkDomainModel = mapper.Map<Walk>(walkRequestDto);
            var updatedWalk = await walkRepository.UpdateWalkAsync(id, walkDomainModel);

            var walkDto = mapper.Map<WalkDto>(updatedWalk);

            Console.WriteLine("--------------------- updatedWalk Walk Name-------------------------");
            Console.WriteLine(updatedWalk.Name);

            return Ok(walkDto);
        }

        // DELETE api/<WalksController>/5
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var walkDomainModel = await walkRepository.deleteWalk(id);
            if (walkDomainModel == null)
            {
                return NotFound("This walk doesn't exist");
            }
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }
    }
}
