using fuelstationAPI.Models;

namespace fuelstationAPI.Interfaces
{
    public interface ILoginRepository
    {
        Task<UserModel?> GetByUserNameAsync(string userName);
    }
}
