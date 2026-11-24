namespace fuelstationAPI.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
    }
}
