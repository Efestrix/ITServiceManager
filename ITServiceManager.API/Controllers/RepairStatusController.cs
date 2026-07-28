using ITServiceManager.API.Dtos.RepairStatus;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITServiceManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RepairStatusController : ControllerBase
    {
        private readonly IRepairStatusService _service;

        public RepairStatusController(IRepairStatusService service)
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
            RepairStatusDto? repairStatus = await _service.GetByIdAsync(id);

            if (repairStatus == null)
                return NotFound();

            return Ok(repairStatus);
        }
    }
}
