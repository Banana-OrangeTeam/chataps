using Chat.Common.DTOs.UserDTOs;
using Chat.Core.Interfaces.Services;
using Chat.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Chat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly IAuthentificationService _authentificationService;
        public AuthentificationController(
            IAuthentificationService authentificationService)
        {
            _authentificationService = authentificationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration(RegistrationDTO registrationDTOs)
        {
            var user = new User()
            {
                UserName = registrationDTOs.UserName,
                Email = registrationDTOs.Email
            };

            await _authentificationService.RegistrationAsync(user, registrationDTOs.Password);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO logDTO)
        {
            var tokens = await _authentificationService.LoginAsync(logDTO);

            return Ok(tokens);
        }
    }
}
