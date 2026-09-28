using ITServiceManager.API.Dtos.RepairOrder;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pomelo.EntityFrameworkCore.MySql.Query.Internal;

namespace ITServiceManager.API.Controllers
{
    [Authorize]
    [Route("api/repair_order")]
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            RepairOrderDto? repairOrder = await _service.GetByIdAsync(id);

            if (repairOrder == null)
                return NotFound();

            return Ok(repairOrder);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRepairOrderDto dto)
        {
            RepairOrderDto createdRepairOrder = await _service.CreateAsync(dto);
            
            if (createdRepairOrder == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), 
                new { id = createdRepairOrder.Id }, 
                createdRepairOrder);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRepairOrderDto dto)
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
