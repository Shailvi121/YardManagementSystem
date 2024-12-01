using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YMS.Dtos;
using YMS.Models;
using YMS.Services.IServices;

namespace YMS.Controllers
{ 

    namespace YMS.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class CoilController : ControllerBase
        {
            private readonly ICoilService _coilService;

            public CoilController(ICoilService coilService)
            {
                _coilService = coilService;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Coil>>> GetCoils()
            {
                var coils = await _coilService.GetAllCoilsAsync();
                return Ok(coils);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Coil>> GetCoil(string id)
            {
                var coil = await _coilService.GetCoilByIdAsync(id);
                if (coil == null) return NotFound();
                return Ok(coil);
            }

            [HttpPost]
            public async Task<IActionResult> SaveCoilAsync([FromBody] CoilDto coilDto)
            {
                if (coilDto.CoilID == "string" && coilDto.CoilID==null)
                {
                    return BadRequest("Invalid coil data.");
                }

                try
                {
                    await _coilService.SaveCoilAsync( coilDto);
                    return Ok("Coil successfully added.");
                }
                catch (Exception ex)
                {
                    // Log the exception if necessary
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }



            [HttpPut("{id}")]
            public async Task<IActionResult> PutCoil(string id, Coil coil)
            {
                if (id != coil.CoilID) return BadRequest();

                var updated = await _coilService.UpdateCoilAsync(coil);
                if (!updated) return NotFound();

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteCoil(string id)
            {
                var deleted = await _coilService.DeleteCoilAsync(id);
                if (!deleted) return NotFound();
                return Ok("Coil successfully deleted.");
            }
        }
    }

}
