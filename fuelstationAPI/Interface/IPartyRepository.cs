using fuelstationAPI.Models;

namespace fuelstationAPI.Interfaces
{
    public interface IPartyRepository
    {
        Task<int> AddPartyAsync(PartyModel model);
        Task<List<PartyModel>> GetPartiesAsync();
        Task<PartyModel?> GetPartyByIdAsync(int partyId);
        Task<bool> UpdatePartyAsync(PartyModel model);
        Task<bool> DeletePartyAsync(int partyId);
    }
}
