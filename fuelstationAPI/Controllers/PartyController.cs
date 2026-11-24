using fuelstationAPI.Interfaces;
using fuelstationAPI.Models;
using fuelstationAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace fuelstationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        private readonly PartyService _service;

        public PartyController(PartyService service)
        {
            _service = service;
        }

        [HttpPost("AddParty")]
        public async Task<IActionResult> AddParty(PartyModel model)
        {
            await _service.AddPartyAsync(model);

            return Ok(new
            {
                success = true,
                message = "Party saved successfully"
            });
        }
    }
}