using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.Device;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Mappings;
using ITServiceManager.API.Middlewares;
using ITServiceManager.API.Services.Interfaces;
using ITServiceManager.API.Validators;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Services
{
    public class DeviceService : BaseService, IDeviceService
    {
        public DeviceService(DatabaseContext context)
            : base(context)
        {
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
            DeviceEntity device = await GetDeviceAsync(id);

            return DeviceMapping.ToDto(device);
        }
        public async Task<DeviceDto> CreateAsync(CreateDeviceDto dto)
        {
            ValidationResult validation = DeviceValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, validation.Errors));

            await ValidateForeignKeys(dto.CustomerId, dto.DeviceTypeId);

            if (await _context.Devices.AnyAsync(d => d.SerialNumber == dto.SerialNumber))
                throw new ValidationException("Device with this serial number already exists.");


            DeviceEntity device = DeviceMapping.ToEntity(dto);

            _context.Devices.Add(device);

            await _context.SaveChangesAsync();

            return DeviceMapping.ToDto(device);
        }

        public async Task DeleteAsync(int id)
        {
            DeviceEntity device = await GetDeviceAsync(id);

            _context.Devices.Remove(device);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, UpdateDeviceDto dto)
        {
            ValidationResult validation = DeviceValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ValidationException(string.Join(Environment.NewLine, validation.Errors));

            DeviceEntity device = await GetDeviceAsync(id);

            await ValidateForeignKeys(dto.CustomerId, dto.DeviceTypeId);

            if (await _context.Devices.AnyAsync(d =>
                d.SerialNumber == dto.SerialNumber &&
                d.Id != id))
            {
                throw new ValidationException("Device with this serial number already exists.");
            }

            DeviceMapping.UpdateEntity(device, dto);

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Vrátí zařízení nebo vyhodí NotFoundException.
        /// </summary>
        private async Task<DeviceEntity> GetDeviceAsync(int id)
        {
            DeviceEntity? device = await _context.Devices.FindAsync(id);

            if (device == null)
                throw new NotFoundException($"Device with id {id} was not found.");

            return device;
        }
        /// <summary>
        /// Ověří existenci cizích klíčů.
        /// </summary>
        private async Task ValidateForeignKeys(int customerId, int deviceTypeId)
        {
            if (!await _context.Customers.AnyAsync(c => c.Id == customerId))
                throw new ValidationException("Customer does not exist.");

            if (!await _context.DeviceTypes.AnyAsync(d => d.Id == deviceTypeId))
                throw new ValidationException("Device type does not exist.");
        }
    }
}
