using fuelstationAPI.Models;

namespace fuelstationAPI.Services
{
    public interface ILoginService
    {
        Task<string?> LoginAsync(LoginRequest request);
    }
}
