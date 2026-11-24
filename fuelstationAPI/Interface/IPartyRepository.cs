using fuelstationAPI.Models;

namespace fuelstationAPI.Interfaces
{
    public interface IPartyRepository
    {
        Task AddPartyAsync(PartyModel model);
    }
}
