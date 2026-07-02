using ITServiceManager.API.Dtos.Device;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITServiceManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _service;

        public DeviceController(IDeviceService service)
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
            DeviceDto? device = await _service.GetByIdAsync(id);

            if (device == null)
                return NotFound();

            return Ok(device);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDeviceDto dto)
        {
            DeviceDto? createdDevice = await _service.CreateAsync(dto);

            if (createdDevice == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), 
                new { id = createdDevice.Id }, 
                createdDevice);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, UpdateDeviceDto dto)
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
