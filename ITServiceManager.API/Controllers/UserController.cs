using ITServiceManager.API.Dtos.User;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITServiceManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            UserDto? user = await _service.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            UserDto createdUser = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = createdUser.Id },
                createdUser);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserDto dto)
        {
            bool success = await _service.UpdateAsync(id, dto);

            if (!success)
                return NotFound();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _service.DeleteAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
