using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.Customer;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Services.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
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
            CustomerDto? customer = await _service.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerDto dto)
        {
            CustomerDto customer = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = customer.Id }, 
                customer);
        }
        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, UpdateCustomerDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return NoContent();
        }
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
