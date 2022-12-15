using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NzWalk.API.Repositories;
using NzWalks.API.Models.DTO;

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
            ValidateWalk(walk);
            if (ModelState.Count > 0)
            {
                return BadRequest(ModelState);
            }
            var walkDomain = mapper.Map<Models.Domain.Walk>(walk);
            walkDomain = await walksRepository.AddWalkAsync(walkDomain);
            var walkDTO = mapper.Map<Models.DTO.Walks>(walkDomain);
            return Ok(walkDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] Models.DTO.WalkRequestBody walk)
        {
            ValidateWalk(walk);
            if(ModelState.Count > 0)
            {
                return BadRequest(ModelState);
            }
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
        private void ValidateWalk(WalkRequestBody walk)
        {
            if(walk == null)
            {
                ModelState.AddModelError("Walk object", "Walk object cannot be null");
            }
            if (string.IsNullOrEmpty(walk.Name))
            {
                ModelState.AddModelError(nameof(walk.Name), $"{nameof(walk.Name)} cannot be null or empty");
            }
            if (walk.Length < 1)
            {
                ModelState.AddModelError(nameof(walk.Length), $"{nameof(walk.Length)} cannot less than 1");
            }
        }
    }
}
