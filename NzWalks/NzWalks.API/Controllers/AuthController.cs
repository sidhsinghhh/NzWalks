using Microsoft.AspNetCore.Mvc;
using NzWalks.API.Models.Domain;
using NzWalks.API.Models.DTO;
using NzWalks.API.Repositories;

namespace NzWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository repository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository repository, ITokenHandler tokenHandler)
        {
            this.repository = repository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(User user)
        {
            var isAuthenticated = await repository.AuthenticateAsync(user.Username, user.Password);
            if (!isAuthenticated)
            {
                return NotFound("Username or Password incorrect");
            }
            var token = await tokenHandler.CreateTokenAsync(user);
            return Ok(token);
        }
    }
}
