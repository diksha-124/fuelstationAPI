using fuelstationAPI.Interfaces;
using fuelstationAPI.Models;

namespace fuelstationAPI.Services
{
    public class PartyService : IPartyService
    {
        private readonly IPartyRepository _repository;

        public PartyService(IPartyRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> AddPartyAsync(PartyModel model)
        {
            Validate(model);
            return await _repository.AddPartyAsync(model);
        }

        public Task<List<PartyModel>> GetPartiesAsync()
        {
            return _repository.GetPartiesAsync();
        }

        public Task<PartyModel?> GetPartyByIdAsync(int partyId)
        {
            if (partyId <= 0)
            {
                throw new ArgumentException("Party id is required");
            }

            return _repository.GetPartyByIdAsync(partyId);
        }

        public async Task<bool> UpdatePartyAsync(PartyModel model)
        {
            if (model.PartyId <= 0)
            {
                throw new ArgumentException("Party id is required");
            }

            Validate(model);
            return await _repository.UpdatePartyAsync(model);
        }

        public Task<bool> DeletePartyAsync(int partyId)
        {
            if (partyId <= 0)
            {
                throw new ArgumentException("Party id is required");
            }

            return _repository.DeletePartyAsync(partyId);
        }

        private static void Validate(PartyModel model)
        {
            if (string.IsNullOrWhiteSpace(model.PartyName))
            {
                throw new ArgumentException("Party name is required");
            }
        }
    }
}
