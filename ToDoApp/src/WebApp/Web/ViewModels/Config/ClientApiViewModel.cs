using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Config
{
    public class ClientApiViewModel : BaseEntityViewModel<Guid>
    {
        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Token { get; set; }

        public DateTime? ExpiredToken { get; set; }
    }
}
