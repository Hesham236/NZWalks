using AutoMapper;
using Microsoft.AspNetCore.Cors;
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
            if (region == null) return NotFound();
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [EnableCors]
        [HttpPost]
        [Route("InsertRegion")]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegion)
        {

            //validation

            if (!AddRegionValidation(addRegion))
            {
                return BadRequest(ModelState);
            }

            //Request(DTO) to domain model

            var region = new Models.Domain.Region()
            {
                Code = addRegion.Code,
                Name = addRegion.Name,
                Area = addRegion.Area,
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
            if (region == null) return NotFound();
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

        [EnableCors]
        [HttpPut]
        [Route("UpdateRegion/{id:Guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,
            [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {

            // validation for update
            if (!UpdateRegionValidation(updateRegionRequest))
            {
                return BadRequest(ModelState);
            }

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
            if (region == null) return NotFound();
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



        #region private Region
        private bool AddRegionValidation(Models.DTO.AddRegionRequest addRegion)
        {
            if (addRegion == null)
            {
                ModelState.AddModelError(nameof(addRegion),
                    $"Add Region Data is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(addRegion.Code))
            {
                ModelState.AddModelError(nameof(addRegion.Code),
                    $"{nameof(addRegion.Code)} cannot be null or empty or white spaced.");
            }
            if (string.IsNullOrWhiteSpace(addRegion.Name))
            {
                ModelState.AddModelError(nameof(addRegion.Name),
                    $"{nameof(addRegion.Name)} cannot be null or empty or white spaced.");
            }
            if (addRegion.Long <= 0)
            {
                ModelState.AddModelError(nameof(addRegion.Long),
                    $"{nameof(addRegion.Long)} cannot be less than zero.");
            }
            if (addRegion.Lat <= 0)
            {
                ModelState.AddModelError(nameof(addRegion.Lat),
                    $"{nameof(addRegion.Lat)} cannot be less than zero..");
            }
            if (addRegion.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegion.Area),
                    $"{nameof(addRegion.Area)} cannot be less than zero..");
            }
            if (addRegion.Population <= 0)
            {
                ModelState.AddModelError(nameof(addRegion.Population),
                    $"{nameof(addRegion.Population)} cannot be less than zero..");
            }


            if (ModelState.Count > 0) return false;

            return true;

        }

        private bool UpdateRegionValidation(Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest),
                    $"Add Region Data is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Code),
                    $"{nameof(updateRegionRequest.Code)} cannot be null or empty or white spaced.");
            }
            if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Name),
                    $"{nameof(updateRegionRequest.Name)} cannot be null or empty or white spaced.");
            }
            if (updateRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Long),
                    $"{nameof(updateRegionRequest.Long)} cannot be less than zero.");
            }
            if (updateRegionRequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Lat),
                    $"{nameof(updateRegionRequest.Lat)} cannot be less than zero..");
            }
            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Area),
                    $"{nameof(updateRegionRequest.Area)} cannot be less than zero..");
            }
            if (updateRegionRequest.Population <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Population),
                    $"{nameof(updateRegionRequest.Population)} cannot be less than zero..");
            }


            if (ModelState.Count > 0) return false;

            return true;

        }
        #endregion
    }
}
