using ITServiceManager.API.Dtos.Device;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITServiceManager.API.Controllers
{
    [Authorize]
    [Route("api/device")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _service;

        public DeviceController(IDeviceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DeviceQueryDto queryDto)
        {
            IEnumerable<DeviceDto> devices =
                await _service.GetAllAsync(queryDto);

            return Ok(devices);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            DeviceDto? device = await _service.GetByIdAsync(id);

            if (device == null)
                return NotFound();

            return Ok(device);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDeviceDto dto)
        {
            DeviceDto? createdDevice = await _service.CreateAsync(dto);

            if (createdDevice == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), 
                new { id = createdDevice.Id }, 
                createdDevice);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDeviceDto dto)
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
