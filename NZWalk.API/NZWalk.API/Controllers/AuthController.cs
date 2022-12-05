using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Reposaitories;

namespace NZWalk.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepo userRepo;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepo userRepo,ITokenHandler tokenHandler)
        {
            this.userRepo = userRepo;
            this.tokenHandler = tokenHandler;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            //validate the incoming request

            //Check if user is authenticated
            //check username and password
            var user = await userRepo.AuthenticateAsync(
                loginRequest.username, loginRequest.password);
            if (user != null)
            {
                //generate a token
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }
            return BadRequest("UserName or Password is incorrect.");
        }
    }
}
