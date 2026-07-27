using ITServiceManager.API.Dtos.RepairHistory;
using ITServiceManager.API.Dtos.RepairOrder;

namespace ITServiceManager.API.Services.Interfaces
{
    public interface IRepairHistoryService
    {
        Task<IEnumerable<RepairHistoryDto>> GetAllAsync();

        Task<RepairHistoryDto> CreateAsync(CreateRepairHistoryDto dto);

        Task UpdateAsync(int id, UpdateRepairHistoryDto dto);
        Task DeleteAsync(int id);
    }
}
