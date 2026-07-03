using ITServiceManager.API.Dtos.RepairHistory;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITServiceManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairHistoryController : ControllerBase
    {
        private readonly IRepairHistoryService _service;

        public RepairHistoryController(IRepairHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRepairHistoryDto dto)
        {
            RepairHistoryDto createdRepairHistory = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetAll), 
                new { id = createdRepairHistory.Id }, 
                createdRepairHistory);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, UpdateRepairHistoryDto dto)
        {
            bool success = await _service.UpdateAsync(id, dto);

            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _service.DeleteAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
