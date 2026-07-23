using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.RepairStatus;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Mappings;
using ITServiceManager.API.Middlewares;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Services
{
    public class RepairStatusService : BaseService, IRepairStatusService
    {
        public RepairStatusService(DatabaseContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<RepairStatusDto>> GetAllAsync()
        {
            List<RepairStatusEntity> repairStatus = await _context.RepairStatus.ToListAsync();

            return repairStatus
                .Select(RepairStatusMapping.ToDto)
                .ToList();
        }

        public async Task<RepairStatusDto?> GetByIdAsync(int id)
        {
            RepairStatusEntity? repairStatus = await _context.RepairStatus.FindAsync(id);

            if (repairStatus == null)
                throw new NotFoundException($"Status with id {id} was not found.");

            return RepairStatusMapping.ToDto(repairStatus);
        }
    }
}
