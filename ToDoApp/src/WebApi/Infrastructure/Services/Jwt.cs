using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApplicationCore.Entities.Config;
using ApplicationCore.Helpers;
using ApplicationCore.Helpers.Jwt.Model;
using ApplicationCore.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    [Authorize(Policy = "Bearer")]
    public class Jwt : IJwt
    {
        private readonly IConfiguration _config;
        public Jwt(IConfiguration config)
        {
            _config = config;
        }

        public JwtResponse GetJwt(ClientApi clientApi, string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, clientApi.Name),
                new Claim(JwtRegisteredClaimNames.UniqueName, clientApi.ClientId),
                new Claim(JwtRegisteredClaimNames.Sid, clientApi.ClientSecret),
                new Claim("Username", username.ToBase64Encode()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"], //Owner Api => Application Name or URL API
                audience: clientApi.ClientId, //Client Token => Client Name or URL Website
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(int.Parse(_config["Jwt:ExpiresInHours"])),
                signingCredentials: credentials);

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);

            var jwtResponse = new JwtResponse
            {
                Token = encodeToken,
                ValidFrom = token.ValidFrom.ToLocalTime(),
                ValidTo = token.ValidTo.ToLocalTime()
            };

            return jwtResponse;
        }
    }
}
