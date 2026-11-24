using fuelstationAPI.Models;
using fuelstationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fuelstationAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        private readonly IPartyService _service;

        public PartyController(IPartyService service)
        {
            _service = service;
        }

        [HttpGet("GetParties")]
        public async Task<IActionResult> GetParties()
        {
            List<PartyModel> parties = await _service.GetPartiesAsync();
            return Ok(parties);
        }

        [HttpGet("GetPartyById/{id}")]
        public async Task<IActionResult> GetPartyById(int id)
        {
            try
            {
                PartyModel? party = await _service.GetPartyByIdAsync(id);
                if (party == null)
                {
                    return NotFound(new { success = false, message = "Party not found" });
                }

                return Ok(party);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("AddParty")]
        public async Task<IActionResult> AddParty(PartyModel model)
        {
            try
            {
                int partyId = await _service.AddPartyAsync(model);
                return Ok(new
                {
                    success = true,
                    partyId,
                    message = "Party saved successfully"
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateParty")]
        public async Task<IActionResult> UpdateParty(PartyModel model)
        {
            try
            {
                bool updated = await _service.UpdatePartyAsync(model);
                if (!updated)
                {
                    return NotFound(new { success = false, message = "Party not found" });
                }

                return Ok(new { success = true, message = "Party updated successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteParty/{id}")]
        public async Task<IActionResult> DeleteParty(int id)
        {
            try
            {
                bool deleted = await _service.DeletePartyAsync(id);
                if (!deleted)
                {
                    return NotFound(new { success = false, message = "Party not found" });
                }

                return Ok(new { success = true, message = "Party deleted successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
