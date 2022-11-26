using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Reposaitories;

namespace NZWalk.API.Controllers
{
    [ApiController]
    [Route("WalkDifficulty")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepo walkDifficultyRepo;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepo walkDifficultyRepo,IMapper mapper) 
        {
            this.walkDifficultyRepo = walkDifficultyRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllWalkDifficulty")]
        public async Task<IActionResult> GetAllWalkDifficultyAsync()
        {
            var wdDomain = await walkDifficultyRepo.GetAllAsync();
            var wdDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(wdDomain);
            return Ok(wdDTO);
        }

        [HttpGet]
        [Route("GetWalkDifficultyById")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            var wdDomain = await walkDifficultyRepo.GetByIdAsync(id);
            if(wdDomain == null) return NotFound();
            var wdDTO = mapper.Map<Models.DTO.WalkDifficulty>(wdDomain);
            return Ok(wdDTO);
        }

        [HttpPost]
        [Route("AddWalkDifficulty")]
        public async Task<IActionResult> AddWalkDifficulty([FromBody] Models.DTO.WalkDifficulty walkDifficulty)
        {
            //Validation For add walk difficulty
            if(!AddWalkDifficultyValidation(walkDifficulty))
            {
                return BadRequest(ModelState);
            }

            var wdDomain = new Models.Domain.WalkDifficulty { Code = walkDifficulty.Code, };
            

            wdDomain = await walkDifficultyRepo.AddWalkDifficultyAsync(wdDomain);

            var wdDTO = new Models.DTO.WalkDifficulty { Code= walkDifficulty.Code, };

            return Ok("New WalkDifficulty Created");
        }

        [HttpPut]
        [Route("UpdateWalkDifficulty/{id:Guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id,
            [FromBody] Models.DTO.UpdateWalkDifficultyRequest updatewalkDifficulty)
        {
            //Validation for update
            if (!UpdateWalkDifficultyValidaion(updatewalkDifficulty))
            {
                return BadRequest(ModelState);
            }
            var wdDomain = new Models.Domain.WalkDifficulty()
            {
                Code = updatewalkDifficulty.Code,
            };

            if (wdDomain == null) return NotFound();

            wdDomain = await walkDifficultyRepo.UpdateWalkDifficultyAsync(id,wdDomain);

            var wdDTO = new Models.DTO.WalkDifficulty()
            {
                Id = wdDomain.Id,            
                Code = wdDomain.Code,
            };

            return Ok("WalkDifficulty Updated");
        }
        
        [HttpDelete]
        [Route("DeleteWalkDifficulty")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var deletewalk = await walkDifficultyRepo.DeleteWalkDifficultyAsync(id);
            if(deletewalk == null) return NotFound();
            var wdDTO = mapper.Map<Models.DTO.WalkDifficulty>(deletewalk);
            return Ok("WalkDifficulty Deleted");
        }

        [HttpGet]
        [Route("GetWalkDiffByName")]
        public async Task<IActionResult> GetWalkDiffByName(string code)
        {
            var wdDomain = await walkDifficultyRepo.GetByNameAsync(code);
            if (wdDomain == null) return NotFound();
            var wdDTO = mapper.Map<Models.DTO.WalkDifficulty> (wdDomain);
            return Ok(wdDTO);
        }

        #region Private Validation

        private bool AddWalkDifficultyValidation(Models.DTO.WalkDifficulty walkDifficulty)
        {
            if (string.IsNullOrWhiteSpace(walkDifficulty.Code))
            {
                ModelState.AddModelError(nameof(walkDifficulty.Code), $"cannot be white spaced or empty");
                return false;
            }

            if (ModelState.Count > 0) return false;
            return true;
        }

        private bool UpdateWalkDifficultyValidaion(Models.DTO.UpdateWalkDifficultyRequest updatewalkDifficulty)
        {
            if(string.IsNullOrWhiteSpace(updatewalkDifficulty.Code)) 
            {
                ModelState.AddModelError(nameof(updatewalkDifficulty.Code), $"Cannot be white spaced or empty");
                return false;
            }
            if(ModelState.Count > 0) return false;

            return true;
        }
        #endregion
    }
}
