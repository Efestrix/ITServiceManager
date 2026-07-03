using ITServiceManager.API.Dtos.Photo;
using ITServiceManager.API.Dtos.RepairHistory;

namespace ITServiceManager.API.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<IEnumerable<PhotoDto>> GetAllAsync();

        Task<PhotoDto> CreateAsync(CreatePhotoDto dto);

        Task<bool> UpdateAsync(int id, UpdatePhotoDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
