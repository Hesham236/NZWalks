using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Reposaitories;

namespace NZWalk.API.Controllers
{
    [ApiController]
    [Route("Regions")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepo regionRepo;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepo regionRepo, IMapper mapper)
        {
            this.regionRepo = regionRepo;
            this.mapper = mapper;
        }


        [HttpGet]
        [Route("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepo.GetAllAsync();
            
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("GetRegionById")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepo.GetAsync(id);
            if(region== null) return NotFound();
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        [Route("InsertRegion")]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegion)
        {
            //Request(DTO) to domain model

            var region = new Models.Domain.Region()
            {
                Code = addRegion.Code,
                Name = addRegion.Name,
                Area= addRegion.Area,
                Long = addRegion.Long,
                Lat = addRegion.Lat,
                Population = addRegion.Population,
            };

            //pass details to repo

            region = await regionRepo.AddRegionAsync(region);

            //convert back to DTO

            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Long = region.Long,
                Lat = region.Lat,
                Population = region.Population,

            };

            //return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id },regionDTO);
            return Ok("Created");
        }

        [HttpDelete]
        [Route("DeleteRegion/{id:Guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //get region by id 
            var region = await regionRepo.DeleteRegionAsync(id);
            //if null not found
            if(region == null) return NotFound();
            //convert response back to dto
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Long = region.Long,
                Lat = region.Lat,
                Population = region.Population,
            };
            //return ok response 
            return Ok("Region Deleted");
        }

        [HttpPut]
        [Route("UpdateRegion/{id:Guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id,
            [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //DTO as domain model
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Long = updateRegionRequest.Long,
                Lat = updateRegionRequest.Lat,
                Population = updateRegionRequest.Population,
            };
            //if null not found
            if(region == null) return NotFound();
            //update using repo
            region = await regionRepo.UpdateRegionAsync(id, region);


            //convert domain Back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Long = region.Long,
                Lat = region.Lat,
                Population = region.Population,

            }; 
            //response ok
            return Ok("Region Updated");

        }
    }
}
