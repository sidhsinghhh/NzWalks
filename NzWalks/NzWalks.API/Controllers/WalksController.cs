using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NzWalk.API.Repositories;

namespace NzWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walksRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walksRepository, IMapper mapper)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() { 
            var walks = await walksRepository.GetAllAsync();
            var walksDTO = mapper.Map<List<Models.DTO.Walks>>(walks);
            return Ok(walksDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await walksRepository.GetWalkAsync(id);
            if(walk == null)
            {
                return BadRequest();
            }
            var walkDTO = mapper.Map<Models.DTO.Walks>(walk);
            return Ok(walkDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalk(Models.DTO.WalkRequestBody walk)
        {
            var walkDomain = mapper.Map<Models.Domain.Walk>(walk);
            walkDomain = await walksRepository.AddWalkAsync(walkDomain);
            var walkDTO = mapper.Map<Models.DTO.Walks>(walkDomain);
            return Ok(walkDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] Models.DTO.WalkRequestBody walk)
        {
            var walkDomain = mapper.Map<Models.Domain.Walk>(walk);
            walkDomain = await walksRepository.UpdateWalkAsync(id, walkDomain);
            if(walkDomain == null)
            {
                return BadRequest();
            }
            var walkDTO = mapper.Map<Models.DTO.Walks>(walkDomain);
            return Ok(walkDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var walk = await walksRepository.DeleteWalkAsync(id);
            if(walk == null)
            {
                return BadRequest();
            }
            var walkDTO = mapper.Map<Models.DTO.Walks>(walk);
            return Ok(walkDTO);
        }
    }
}
