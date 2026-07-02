using ITServiceManager.API.Dtos.Device;
using ITServiceManager.API.Entities;

namespace ITServiceManager.API.Mappings
{
    public static class DeviceMapping
    {
        public static DeviceDto ToDto(DeviceEntity entity)
        {
            return new DeviceDto
            {
                Id = entity.Id,
                Manufacturer = entity.Manufacturer,
                Model = entity.Model,
                SerialNumber = entity.SerialNumber,
                PurchaseDate = entity.PurchaseDate.ToDateTime(TimeOnly.MinValue),
                CustomerId = entity.CustomerId,
                DeviceTypeId = entity.DeviceTypeId
            };
        }
        public static DeviceEntity ToEntity(CreateDeviceDto dto)
        {
            return new DeviceEntity
            {
                Manufacturer = dto.Manufacturer,
                Model = dto.Model,
                SerialNumber = dto.SerialNumber,
                PurchaseDate = DateOnly.FromDateTime(dto.PurchaseDate),
                CustomerId = dto.CustomerId,
                DeviceTypeId = dto.DeviceTypeId
            };
        }
        public static void UpdateEntity(DeviceEntity entity, UpdateDeviceDto dto)
        {
            entity.Manufacturer = dto.Manufacturer;
            entity.Model = dto.Model;
            entity.SerialNumber = dto.SerialNumber;
            entity.PurchaseDate = DateOnly.FromDateTime(dto.PurchaseDate);
            entity.CustomerId = dto.CustomerId;
            entity.DeviceTypeId = dto.DeviceTypeId;
        }
    }
}
