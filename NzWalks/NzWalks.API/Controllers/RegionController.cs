using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NzWalks.API.Models.DTO;
using NzWalks.API.Repositories;

namespace NzWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            //fetch from repository and return the DTO
            var regions = await regionRepository.GetAllAsync();

            //converting to DTO
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            
            return Ok(regionsDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetRegionAsync(id);
            if(region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddRegionAsync([FromBody] RegionRequestBody region)
        {
            var regionDomain = mapper.Map<Models.Domain.Region>(region);
            regionDomain = await regionRepository.AddRegionAsync(regionDomain);
            var regionDTO = mapper.Map<Models.DTO.Region>(regionDomain);
            return Ok(regionDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var region = await regionRepository.DeleteRegionAsync(id);
            if(region == null)
            {
                return NotFound();  
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] RegionRequestBody region)
        {
            var regionDomain = mapper.Map<Models.Domain.Region>(region);
            regionDomain = await regionRepository.UpdateRegionAsync(id, regionDomain);
            if(regionDomain== null)
            {
                return NotFound();
            }
            return Ok(regionDomain);
        }
    }
}
