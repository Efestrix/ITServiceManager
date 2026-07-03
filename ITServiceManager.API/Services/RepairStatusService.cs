using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.RepairStatus;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Mappings;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Services
{
    public class RepairStatusService : IRepairStatusService
    {
        private readonly DatabaseContext _context;

        public RepairStatusService(DatabaseContext context)
        {
            _context = context;
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
            bool exists = await _context.RepairStatus.AnyAsync(rs => rs.Id == id);

            if (!exists)
                return null;

            RepairStatusEntity? repairStatus = await _context.RepairStatus.FindAsync(id);

            if (repairStatus == null)
                return null;

            return RepairStatusMapping.ToDto(repairStatus);
        }
    }
}
