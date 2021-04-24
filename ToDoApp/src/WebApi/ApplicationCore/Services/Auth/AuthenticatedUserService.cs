using ApplicationCore.Interfaces.Auth;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ApplicationCore.Services.Auth
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public string Username { get; }
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            Username = httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "Username")?.Value;
        }
    }
}
