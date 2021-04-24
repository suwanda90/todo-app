using ApplicationCore.Helpers.Jwt.Model;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<JwtResponse> GenerateJwtAsync(string clientId, string clientSecret, string username);
    }
}
