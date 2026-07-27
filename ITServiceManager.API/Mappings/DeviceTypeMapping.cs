using ITServiceManager.API.Dtos.Customer;
using ITServiceManager.API.Dtos.DeviceType;
using ITServiceManager.API.Entities;

namespace ITServiceManager.API.Mappings
{
    public static class DeviceTypeMapping
    {
        public static DeviceTypeDto ToDto(DeviceTypeEntity entity)
        {
            return new DeviceTypeDto
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        public static DeviceTypeEntity ToEntity(CreateDeviceTypeDto dto)
        {
            return new DeviceTypeEntity
            {
                Name = dto.Name
            };
        }

        public static void UpdateEntity(DeviceTypeEntity entity, UpdateDeviceTypeDto dto)
        {
            entity.Name = dto.Name;
        }
    }
}
