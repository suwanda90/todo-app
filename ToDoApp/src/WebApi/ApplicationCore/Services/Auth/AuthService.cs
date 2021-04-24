using ApplicationCore.Entities.Config;
using ApplicationCore.Helpers.Jwt.Model;
using ApplicationCore.Interfaces.Auth;
using ApplicationCore.Interfaces.BaseEntity;
using ApplicationCore.Specifications.Config;
using System;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IEfRepository<ClientApi, Guid> _clientApiRepository;
        private readonly IJwt _jwt;

        public AuthService(IEfRepository<ClientApi, Guid> clientApiRepository, IJwt jwt)
        {
            _clientApiRepository = clientApiRepository;
            _jwt = jwt;
        }

        public async Task<JwtResponse> GenerateJwtAsync(string clientId, string clientSecret, string username)
        {
            var jwtResponse = new JwtResponse();
            var spec = new ClientApiSpecification(clientId, clientSecret);
            var clientApi = await _clientApiRepository.GetAsync(spec);

            if (clientApi != null)
            {
                jwtResponse = _jwt.GetJwt(clientApi, username);

                //update ClientApi
                clientApi.Token = jwtResponse.Token;
                clientApi.ExpiredToken = jwtResponse.ValidTo;
                clientApi.ModifiedBy = username;
                await _clientApiRepository.UpdateAsync(clientApi);
            }

            return jwtResponse;
        }
    }
}
