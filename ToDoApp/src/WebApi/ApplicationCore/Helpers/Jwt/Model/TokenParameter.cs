using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Helpers.Jwt.Model
{
    public class TokenParameter
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
