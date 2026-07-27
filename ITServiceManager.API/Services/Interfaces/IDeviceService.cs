using ITServiceManager.API.Dtos.Customer;
using ITServiceManager.API.Dtos.Device;

namespace ITServiceManager.API.Services.Interfaces
{
    public interface IDeviceService
    {
        Task<IEnumerable<DeviceDto>> GetAllAsync();

        Task<DeviceDto?> GetByIdAsync(int id);

        Task<DeviceDto> CreateAsync(CreateDeviceDto dto);

        Task UpdateAsync(int id, UpdateDeviceDto dto);
        Task DeleteAsync(int id);
    }
}
