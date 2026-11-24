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

        public async Task AddPartyAsync(PartyModel model)
        {
            if (string.IsNullOrWhiteSpace(model.PartyName))
            {
                throw new ArgumentException("Party name is required");
            }

            await _repository.AddPartyAsync(model);
        }
    }
}