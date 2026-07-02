using ITServiceManager.API.Dtos.Customer;
using ITServiceManager.API.Dtos.RepairOrder;
using ITServiceManager.API.Entities;

namespace ITServiceManager.API.Mappings
{
    public static class RepairOrderMapping
    {
        public static RepairOrderDto ToDto(RepairOrderEntity entity)
        {
            return new RepairOrderDto
            {
                Id = entity.Id,
                OrderNumber = entity.OrderNumber,
                Description = entity.Description,
                Price = entity.Price,
                CreatedAt = entity.CreatedAt,
                FinishedAt = entity.FinishedAt,
                DeviceId = entity.DeviceId,

            };
        }

        public static RepairOrderEntity ToEntity(CreateRepairOrderDto dto)
        {
            return new RepairOrderEntity
            {
                OrderNumber = dto.OrderNumber,
                Description = dto.Description,
                Price = dto.Price,
                CreatedAt = dto.CreatedAt,
                FinishedAt = dto.FinishedAt,
                DeviceId = dto.DeviceId,
                TechnicianId = dto.TechnicianId,
                StatusId = dto.StatusId
            };
        }

        public static void UpdateEntity(RepairOrderEntity entity, UpdateRepairOrderDto dto)
        {
            entity.OrderNumber = dto.OrderNumber;
            entity.Description = dto.Description;
            entity.Price = dto.Price;
            entity.CreatedAt = dto.CreatedAt;
            entity.FinishedAt = dto.FinishedAt;
            entity.DeviceId = dto.DeviceId;
            entity.TechnicianId = dto.TechnicianId;
            entity.StatusId = dto.StatusId;
        }
    }
}
