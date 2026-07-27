using ITServiceManager.API.Dtos.Photo;
using ITServiceManager.API.Entities;
using System.Diagnostics;

namespace ITServiceManager.API.Mappings
{
    public static class PhotoMapping
    {
        public static PhotoDto ToDto(PhotoEntity entity)
        {
            return new PhotoDto
            {
                Id = entity.Id,
                RepairOrderId = entity.RepairOrderId,
                FileName = entity.FileName,
                Description = entity.Description
            };
        }
        public static PhotoEntity ToEntity(CreatePhotoDto dto)
        {
            return new PhotoEntity
            {
                RepairOrderId = dto.RepairOrderId,
                FileName = dto.FileName,
                Description = dto.Description
            };
        }
        public static void UpdateEntity(PhotoEntity entity, UpdatePhotoDto dto)
        {
            entity.RepairOrderId = dto.RepairOrderId;
            entity.FileName = dto.FileName;
            entity.Description = dto.Description;
        }
    }
}
