using System.ComponentModel.DataAnnotations;

namespace fuelstationAPI.Models
{
    public class PartyModel
    {
        public int PartyId { get; set; }

        [Required]
        [StringLength(100)]
        public string PartyName { get; set; } = string.Empty;

        [StringLength(15)]
        public string? MobileNo { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        public decimal CreditLimit { get; set; }

        public PartyModel()
        {
            CreditLimit = 0;
        }
    }
}
