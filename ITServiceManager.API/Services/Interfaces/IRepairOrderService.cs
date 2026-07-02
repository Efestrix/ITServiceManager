using ITServiceManager.API.Dtos.Device;
using ITServiceManager.API.Dtos.RepairOrder;

namespace ITServiceManager.API.Services.Interfaces
{
    public interface IRepairOrderService
    {
        Task<IEnumerable<RepairOrderDto>> GetAllAsync();

        Task<RepairOrderDto?> GetByIdAsync(int id);

        Task<RepairOrderDto> CreateAsync(CreateRepairOrderDto dto);

        Task<bool> UpdateAsync(int id, UpdateRepairOrderDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
