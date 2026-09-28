using ITServiceManager.API.Dtos.RepairHistory;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITServiceManager.API.Controllers
{
    [Authorize]
    [Route("api/repair_history")]
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
        public async Task<IActionResult> Create([FromBody] CreateRepairHistoryDto dto)
        {
            RepairHistoryDto createdRepairHistory = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetAll), 
                new { id = createdRepairHistory.Id }, 
                createdRepairHistory);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRepairHistoryDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
