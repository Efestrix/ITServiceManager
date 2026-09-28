using ITServiceManager.API.Dtos.DeviceType;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITServiceManager.API.Controllers
{
    [Authorize]
    [Route("api/device_type")]
    [ApiController]
    public class DeviceTypeController : ControllerBase
    {
        private readonly IDeviceTypeService _service;

        public DeviceTypeController(IDeviceTypeService service)
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
            DeviceTypeDto? deviceType = await _service.GetByIdAsync(id);

            if (deviceType == null)
                return NotFound();

            return Ok(deviceType);
        }
    }
}
