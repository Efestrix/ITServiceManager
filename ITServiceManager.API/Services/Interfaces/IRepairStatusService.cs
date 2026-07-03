using ITServiceManager.API.Dtos.RepairStatus;

namespace ITServiceManager.API.Services.Interfaces
{
    public interface IRepairStatusService
    {
        Task<IEnumerable<RepairStatusDto>> GetAllAsync();
        Task<RepairStatusDto?> GetByIdAsync(int id);
    }
}
