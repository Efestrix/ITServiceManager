using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.Photo;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Mappings;
using ITServiceManager.API.Services.Interfaces;
using ITServiceManager.API.Validators;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly DatabaseContext _context;

        public PhotoService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PhotoDto>> GetAllAsync()
        {
            List<PhotoEntity> photos = await _context.Photos.ToListAsync();

            return photos
                .Select(PhotoMapping.ToDto)
                .ToList();
        }
        public async Task<PhotoDto> CreateAsync(CreatePhotoDto dto)
        {
            ValidationResult validation = PhotoValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, validation.Errors));

            PhotoEntity entity = PhotoMapping.ToEntity(dto);

            _context.Photos.Add(entity);

            await _context.SaveChangesAsync();

            return PhotoMapping.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool photo = await _context.Photos.AnyAsync(p => p.Id == id);

            if (!photo)
                return false;

            PhotoEntity? entity = await _context.Photos.FindAsync(id);

            if (entity == null)
                return false;

            _context.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> UpdateAsync(int id, UpdatePhotoDto dto)
        {
            ValidationResult validation = PhotoValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, validation.Errors));

            PhotoEntity? entity = await _context.Photos.FindAsync(id);

            if (entity == null)
                return false;

            PhotoMapping.UpdateEntity(entity, dto);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
