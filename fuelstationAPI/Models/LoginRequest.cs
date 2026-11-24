using System.ComponentModel.DataAnnotations;

namespace fuelstationAPI.Models
{
    public class LoginRequest
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
