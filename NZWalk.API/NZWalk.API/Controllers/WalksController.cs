using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalk.API.Models.DTO;
using NZWalk.API.Reposaitories;

namespace NZWalk.API.Controllers
{
    [ApiController]
    [Route("Walks")]
    public class WalksController : Controller
    {
        private readonly IWalkRepo walkRepo;
        private readonly IMapper mapper;
        private readonly IRegionRepo regionRepo;
        private readonly IWalkDifficultyRepo walkDifficultyRepo;

        public WalksController(IWalkRepo walkRepo,IMapper mapper,
            IRegionRepo regionRepo,IWalkDifficultyRepo walkDifficultyRepo)
        {
            this.walkRepo = walkRepo;
            this.mapper = mapper;
            this.regionRepo = regionRepo;
            this.walkDifficultyRepo = walkDifficultyRepo;
        }
        
        [HttpGet]
        [Route("GetAllWalks")]
        public async Task<IActionResult> GetAllWalksAsync()
        {
           //fetch domain walk
           var walkdomain = await walkRepo.GetAllAsync();
           // map to wald DTO
           var walkDTO = mapper.Map<List<Models.DTO.Walk>>(walkdomain);
           //return response
           return Ok(walkDTO);
        }
        [HttpGet]
        [Route("GetWalkById/{id:Guid}")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walkdomain = await walkRepo.GetWalkAsync(id);
            if (walkdomain == null) return NotFound();
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkdomain);
            return Ok(walkDTO);

        }
        [EnableCors]
        [HttpPost]
        [Route("AddWalk")]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {

            //validation on add walk
            if(!(await AddWalkValidation(addWalkRequest)))
            {
                return BadRequest(ModelState);
            }

            //Convert Dto to Domain Model
            var walkdomain = new Models.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name= addWalkRequest.Name,
                RegionId=addWalkRequest.RegionId,
                WalkDifficultyId=addWalkRequest.WalkDifficultyId,
            };

            //pass domain object to repo
            walkdomain = await walkRepo.AddWalkAsync(walkdomain);

            //convert the domain object back to Dto 
            var walkDTO = new Models.DTO.Walk
            {
                Id = walkdomain.Id,
                Name = walkdomain.Name,
                RegionId = walkdomain.RegionId,
                WalkDifficultyId = walkdomain.WalkDifficultyId,
            };
            //Send DTO Response Back
            return Ok("New Walk Created");
        }
        [HttpDelete]
        [Route("DeleteWalk/{id:Guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var deletedwalk = await walkRepo.DeleteWalkAsync(id);
            if(deletedwalk == null) return NotFound();
            var walkDTO = mapper.Map<Models.DTO.Walk>(deletedwalk);
            return Ok("Walk Deleted");
        }
        [EnableCors]
        [HttpPut]
        [Route("UpdateWalk/{id:Guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id,
            [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {

            //validation on update
            if(!(await UpdateWalkValidation(updateWalkRequest)))
            {
                return BadRequest(ModelState);
            }
            //Convert DTO To Domain Object
            var walkdomain = new Models.Domain.Walk 
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
            };
            //pass details to repo - get domain object in response 
            walkdomain = await walkRepo.UpdateWalkAsync(id, walkdomain);
            //handle null not found
            if(walkdomain == null) return NotFound();
            else
            {
                //convert back to dto
                var walkDTO = new Models.DTO.Walk
                {
                    Id = walkdomain.Id,
                    Length= walkdomain.Length,
                    Name = walkdomain.Name,
                    RegionId = walkdomain.RegionId,
                    WalkDifficultyId= walkdomain.WalkDifficultyId,
                };
            }
            //return response
            return Ok("Updated successfuly");
        }



        #region Private Validation 

        private async Task<bool> AddWalkValidation(Models.DTO.AddWalkRequest addWalkRequest)
        {
            if (addWalkRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest),
                    $"Add Walk Data is required");
                return false;
            }
            if (addWalkRequest.Length <= 0)
            {
                ModelState.AddModelError(nameof(addWalkRequest.Length),
                    $"{nameof(addWalkRequest.Length)} cannot be Less Than zero.");
            }
            if (string.IsNullOrWhiteSpace(addWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(addWalkRequest.Name),
                    $"{nameof(addWalkRequest.Name)} cannot be null or empty or white spaced.");
            }
            var region = await regionRepo.GetAsync(addWalkRequest.RegionId);
            if (region == null) 
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId),
                    $"{nameof(addWalkRequest.RegionId)} is not avaialble.");
            }
            var walkDiff = await walkDifficultyRepo.GetByIdAsync(addWalkRequest.WalkDifficultyId);
            if(walkDiff == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId),
                    $"{nameof(addWalkRequest.WalkDifficultyId)} is not avaialble.");
            }

            if (ModelState.Count > 0) return false;

            return true;

        }

        private async Task<bool> UpdateWalkValidation(Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            if (updateWalkRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest),
                    $"Add Walk Data is required");
                return false;
            }
            if (updateWalkRequest.Length <= 0)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Length),
                    $"{nameof(updateWalkRequest.Length)} cannot be Less Than zero.");
            }
            if (string.IsNullOrWhiteSpace(updateWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Name),
                    $"{nameof(updateWalkRequest.Name)} cannot be null or empty or white spaced.");
            }
            var region = await regionRepo.GetAsync(updateWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId),
                    $"{nameof(updateWalkRequest.RegionId)} is not avaialble.");
            }
            var walkDiff = await walkDifficultyRepo.GetByIdAsync(updateWalkRequest.WalkDifficultyId);
            if (walkDiff == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId),
                    $"{nameof(updateWalkRequest.WalkDifficultyId)} is not avaialble.");
            }
            
            if (ModelState.Count > 0) return false;

            return true;

        }

        #endregion
    }
}
