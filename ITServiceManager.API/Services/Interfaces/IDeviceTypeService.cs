using ITServiceManager.API.Dtos.DeviceType;

namespace ITServiceManager.API.Services.Interfaces
{
    public interface IDeviceTypeService
    {
        Task<IEnumerable<DeviceTypeDto>> GetAllAsync();
        Task<DeviceTypeDto?> GetByIdAsync(int id);
    }
}
