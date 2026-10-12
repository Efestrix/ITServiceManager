using ITServiceManager.API.Dtos.Photo;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITServiceManager.API.Controllers
{
    [Authorize]
    [Route("api/photo")]
    [ApiController]
    [Authorize(Roles = "Admin,Technician")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _service;
        public PhotoController(IPhotoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePhotoDto dto)
        {
            PhotoDto createdPhoto = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetAll),
                new { id = createdPhoto.Id },
                createdPhoto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePhotoDto dto)
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
