using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NzWalks.API.Models.Domain;
using NzWalks.API.Models.DTO;
using NzWalks.API.Repositories;

namespace NzWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository repository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultyAsync()
        {
            //get walkdifficulty domain 
            var walkDifficulties = await repository.GetAllAsync();

            var WalkDifficultiesDTO = mapper.Map<List<WalkDifficultyDTO>>(walkDifficulties);
            return Ok(WalkDifficultiesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAllWalkDifficultyAsync(Guid id)
        {
            //get walkdifficulty domain 
            var walkDifficulty = await repository.GetWalkDifficultyAsync(id);
            if(walkDifficulty == null)
            {
                return BadRequest();
            }
            var WalkDifficultyDTO = mapper.Map<WalkDifficultyDTO>(walkDifficulty);
            return Ok(WalkDifficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficulty(WalkDifficultyRequestBody walkDifficultyRequestBody)
        {
            ValidateWalkDifficulty(walkDifficultyRequestBody);
            if(ModelState.Count > 0)
            {
                return BadRequest(ModelState);
            }
            var walkDifficultyDomain = mapper.Map<WalkDifficulty>(walkDifficultyRequestBody);
            var walkDifficulty = await repository.AddWalkDifficultyAsync(walkDifficultyDomain);
            var WalkDifficultyDTO = mapper.Map<WalkDifficultyDTO>(walkDifficulty);
            return Ok(WalkDifficultyDTO);   
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficulty([FromRoute] Guid id, [FromBody] WalkDifficultyRequestBody walkDifficultyRequestBody)
        {
            var walkDifficultyDomain = mapper.Map<WalkDifficulty>(walkDifficultyRequestBody);
            var walkDifficulty = await repository.UpdateWalkDifficulty(id, walkDifficultyDomain);
            if(walkDifficulty!= null)
            {
                return BadRequest(walkDifficulty);
            }
            var walkDifficultyDTO = mapper.Map<WalkDifficultyDTO>(walkDifficulty);
            return Ok(walkDifficultyDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            var walkDifficulty = await repository.DeleteWalkDifficultyAsync(id);
            if(walkDifficulty== null )
            {
                return BadRequest();
            }
            var walkDifficultyDTO = mapper.Map<WalkDifficultyDTO>(walkDifficulty);
            return Ok(walkDifficultyDTO);   
        }

        private void ValidateWalkDifficulty(WalkDifficultyRequestBody walkDifficulty)
        {
            if(walkDifficulty == null)
            {
                ModelState.AddModelError("walk Difficulty", "walk difficulty cannot be null");
            }
            if (string.IsNullOrWhiteSpace(walkDifficulty.Code))
            {
                ModelState.AddModelError(nameof(walkDifficulty), $"{nameof(walkDifficulty.Code)} cannot be null or whitespace");
            }
        }
    }
}
