using ApplicationCore.Helpers;
using ApplicationCore.Helpers.Jwt.Model;
using ApplicationCore.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration config, IAuthService authService)
        {
            _config = config;
            _authService = authService;
        }

        // GET: api/Auth/token
        [HttpPost("token")]
        public async Task<ActionResult> Token([FromBody] TokenParameter param)
        {
            ActionResult response = Unauthorized();

            if (param.ClientSecret.IsBase64())
            {
                var jwtResponse = await _authService.GenerateJwtAsync(param.ClientId, param.ClientSecret.ToBase64DecodeWithKey(_config["Security:EncryptKey"]), param.Username);

                if (!string.IsNullOrEmpty(jwtResponse.Token))
                {
                    response = Ok(jwtResponse);
                }
            }

            return response;
        }
    }
}
