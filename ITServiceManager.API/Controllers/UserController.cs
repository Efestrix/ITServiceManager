using ITServiceManager.API.Dtos.User;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITServiceManager.API.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] UserQueryDto queryDto)
        {
            IEnumerable<UserDto> users = 
                await _service.GetAllAsync(queryDto);

            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            UserDto? user = await _service.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            UserDto createdUser = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = createdUser.Id },
                createdUser);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserDto dto)
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
