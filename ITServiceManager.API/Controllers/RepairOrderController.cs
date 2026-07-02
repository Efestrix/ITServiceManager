using ITServiceManager.API.Dtos.RepairOrder;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pomelo.EntityFrameworkCore.MySql.Query.Internal;

namespace ITServiceManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairOrderController : ControllerBase
    {
        private readonly IRepairOrderService _service;

        public RepairOrderController(IRepairOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            RepairOrderDto? repairOrder = await _service.GetByIdAsync(id);

            if (repairOrder == null)
                return NotFound();

            return Ok(repairOrder);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRepairOrderDto dto)
        {
            RepairOrderDto createdRepairOrder = await _service.CreateAsync(dto);
            
            if (createdRepairOrder == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), 
                new { id = createdRepairOrder.Id }, 
                createdRepairOrder);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, UpdateRepairOrderDto dto)
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
