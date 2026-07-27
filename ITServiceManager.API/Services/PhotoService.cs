using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.Photo;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Mappings;
using ITServiceManager.API.Middlewares;
using ITServiceManager.API.Services.Interfaces;
using ITServiceManager.API.Validators;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Services
{
    public class PhotoService : BaseService, IPhotoService
    {
        public PhotoService(DatabaseContext context)
            : base(context)
        {
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

            await ValidateRepairOrder(dto.RepairOrderId);

            PhotoEntity entity = PhotoMapping.ToEntity(dto);

            _context.Photos.Add(entity);

            await _context.SaveChangesAsync();

            return PhotoMapping.ToDto(entity);
        }

        public async Task DeleteAsync(int id)
        {
            PhotoEntity photo = await GetPhotoAsync(id);

            _context.Photos.Remove(photo);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, UpdatePhotoDto dto)
        {
            ValidationResult validation = PhotoValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, validation.Errors));

            PhotoEntity? entity = await _context.Photos.FindAsync(id);

            await ValidateRepairOrder(dto.RepairOrderId);

            PhotoMapping.UpdateEntity(entity, dto);

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Vrátí fotografii nebo vyhodí NotFoundException.
        /// </summary>
        private async Task<PhotoEntity> GetPhotoAsync(int id)
        {
            PhotoEntity? photo = await _context.Photos.FindAsync(id);

            if (photo == null)
                throw new NotFoundException($"Photo with id {id} was not found.");

            return photo;
        }

        /// <summary>
        /// Ověří existenci servisní zakázky.
        /// </summary>
        private async Task ValidateRepairOrder(int repairOrderId)
        {
            if (!await _context.RepairOrders.AnyAsync(r => r.Id == repairOrderId))
                throw new ValidationException("Repair order does not exist.");
        }
    }
}
