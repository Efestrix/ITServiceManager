using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.DeviceType;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Mappings;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Services
{
    public class DeviceTypeService : BaseService, IDeviceTypeService
    {
        public DeviceTypeService(DatabaseContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<DeviceTypeDto>> GetAllAsync()
        {
            List<DeviceTypeEntity> deviceTypes = await _context.DeviceTypes.ToListAsync();

            return deviceTypes
                .Select(DeviceTypeMapping.ToDto)
                .ToList();
        }

        public async Task<DeviceTypeDto?> GetByIdAsync(int id)
        {
            bool deviceType = await _context.DeviceTypes.AnyAsync(d => d.Id == id);

            if (!deviceType)
                return null;
            
            DeviceTypeEntity? deviceTypeEntity = await _context.DeviceTypes.FindAsync(id);

            if (deviceTypeEntity == null)
                return null;

            return DeviceTypeMapping.ToDto(deviceTypeEntity);
        }
    }
}
