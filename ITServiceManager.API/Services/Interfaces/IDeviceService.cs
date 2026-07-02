using ITServiceManager.API.Dtos.Customer;
using ITServiceManager.API.Dtos.Device;

namespace ITServiceManager.API.Services.Interfaces
{
    public interface IDeviceService
    {
        Task<IEnumerable<DeviceDto>> GetAllAsync();

        Task<DeviceDto?> GetByIdAsync(int id);

        Task<DeviceDto> CreateAsync(CreateDeviceDto dto);

        Task<bool> UpdateAsync(int id, UpdateDeviceDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
