namespace fuelstationAPI.Models
{
    public class PartyModel
    {
        public int PartyId { get; set; }

        public string PartyName { get; set; }
         
        public string MobileNo { get; set; }

        public string City { get; set; }

        public decimal CreditLimit { get; set; }

        public PartyModel()

        {
            CreditLimit = 0;
        }
    }
}
