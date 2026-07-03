using ITServiceManager.API.Dtos.RepairHistory;
using ITServiceManager.API.Entities;

namespace ITServiceManager.API.Mappings
{
    public static class RepairHistoryMapping
    {
        public static RepairHistoryDto ToDto(RepairHistoryEntity entity)
        {
            return new RepairHistoryDto
            {
                Id = entity.Id,
                RepairOrderId = entity.RepairOrderId,
                CreatedAt = entity.CreatedAt,
                Description = entity.Description,
                HoursWorked = entity.HoursWorked
            };
        }
        public static RepairHistoryEntity ToEntity(CreateRepairHistoryDto dto)
        {
            return new RepairHistoryEntity
            {
                RepairOrderId = dto.RepairOrderId,
                CreatedAt = dto.CreatedAt,
                Description = dto.Description,
                HoursWorked = dto.HoursWorked
            };
        }
        public static void UpdateEntity(RepairHistoryEntity entity, UpdateRepairHistoryDto dto)
        {
            entity.RepairOrderId = dto.RepairOrderId;
            entity.CreatedAt = dto.CreatedAt;
            entity.Description = dto.Description;
            entity.HoursWorked = dto.HoursWorked;
        }
    }
}
