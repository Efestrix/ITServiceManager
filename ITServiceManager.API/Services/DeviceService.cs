using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.Device;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Mappings;
using ITServiceManager.API.Services.Interfaces;
using ITServiceManager.API.Validators;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly DatabaseContext _context;

        public DeviceService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DeviceDto>> GetAllAsync()
        {
            List<DeviceEntity> devices = await _context.Devices.ToListAsync();

            return devices
                .Select(DeviceMapping.ToDto)
                .ToList();
        }
        public async Task<DeviceDto?> GetByIdAsync(int id)
        {
            bool deviceExists = await _context.Devices.AnyAsync(c => c.Id == id);

            if (!deviceExists)
                return null;

            DeviceEntity? device = await _context.Devices.FindAsync(id);

            if (device == null)
                return null;

            return DeviceMapping.ToDto(device);
        }
        public async Task<DeviceDto> CreateAsync(CreateDeviceDto dto)
        {
            ValidationResult validation = DeviceValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, validation.Errors));

            DeviceEntity device = DeviceMapping.ToEntity(dto);

            _context.Devices.Add(device);

            await _context.SaveChangesAsync();

            return DeviceMapping.ToDto(device);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool deviceExists = await _context.Devices.AnyAsync(c => c.Id == id);

            if (!deviceExists)
                return false;

            DeviceEntity? device = await _context.Devices.FindAsync(id);

            if (device == null)
                return false;

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> UpdateAsync(int id, UpdateDeviceDto dto)
        {
            bool deviceExists = await _context.Devices.AnyAsync(c => c.Id == id);

            if (!deviceExists)
                return false;

            ValidationResult validation = DeviceValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, validation.Errors));

            DeviceEntity? device = await _context.Devices.FindAsync(id);

            if (device == null)
                return false;

            DeviceMapping.UpdateEntity(device, dto);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
