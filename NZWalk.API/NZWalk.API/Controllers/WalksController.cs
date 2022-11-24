using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalk.API.Reposaitories;

namespace NZWalk.API.Controllers
{
    [ApiController]
    [Route("Walks")]
    public class WalksController : Controller
    {
        private readonly IWalkRepo walkRepo;
        private readonly IMapper mapper;

        public WalksController(IWalkRepo walkRepo,IMapper mapper)
        {
            this.walkRepo = walkRepo;
            this.mapper = mapper;
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
        [HttpPost]
        [Route("AddWalk")]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
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
        [HttpPut]
        [Route("UpdateWalk/{id:Guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
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
    }
}
