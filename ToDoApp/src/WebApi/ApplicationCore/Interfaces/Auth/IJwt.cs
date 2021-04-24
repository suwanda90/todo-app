using ApplicationCore.Entities.Config;
using ApplicationCore.Helpers.Jwt.Model;

namespace ApplicationCore.Interfaces.Auth
{
    public interface IJwt
    {
        JwtResponse GetJwt(ClientApi clientApi, string username);
    }
}
