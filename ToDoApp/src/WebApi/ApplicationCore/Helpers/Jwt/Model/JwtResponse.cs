using System;

namespace ApplicationCore.Helpers.Jwt.Model
{
    public class JwtResponse
    {
        public string Token { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
