using ITServiceManager.API.Dtos.DeviceType;
using ITServiceManager.API.Dtos.RepairStatus;
using ITServiceManager.API.Entities;

namespace ITServiceManager.API.Mappings
{
    public static class RepairStatusMapping
    {
        public static RepairStatusDto ToDto(RepairStatusEntity entity)
        {
            return new RepairStatusDto
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        public static RepairStatusEntity ToEntity(CreateRepairStatusDto dto)
        {
            return new RepairStatusEntity
            {
                Name = dto.Name
            };
        }

        public static void UpdateEntity(RepairStatusEntity entity, UpdateRepairStatusDto dto)
        {
            entity.Name = dto.Name;
        }
    }
}
