using ITServiceManager.API.Dtos.Photo;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITServiceManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> Create(CreatePhotoDto dto)
        {
            PhotoDto createdPhoto = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetAll),
                new { id = createdPhoto.Id },
                createdPhoto);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, UpdatePhotoDto dto)
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
