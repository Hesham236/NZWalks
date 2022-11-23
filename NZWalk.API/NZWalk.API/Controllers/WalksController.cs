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
        [Route("GetWalkById")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walkdomain = await walkRepo.GetWalkAsync(id);
            if (walkdomain == null) return NotFound();
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkdomain);
            return Ok(walkDTO);

        }
    }
}
