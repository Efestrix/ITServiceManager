using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.RepairHistory;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Mappings;
using ITServiceManager.API.Services.Interfaces;
using ITServiceManager.API.Validators;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Services
{
    public class RepairHistoryService : IRepairHistoryService
    {
        private readonly DatabaseContext _context;

        public RepairHistoryService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RepairHistoryDto>> GetAllAsync()
        {
            List<RepairHistoryEntity> repairHistories = await _context.RepairHistory.ToListAsync();

            return repairHistories.Select(RepairHistoryMapping.ToDto).ToList();
        }
        public async Task<RepairHistoryDto> CreateAsync(CreateRepairHistoryDto dto)
        {
            ValidationResult validation = RepairHistoryValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, validation.Errors));

            RepairHistoryEntity entity = RepairHistoryMapping.ToEntity(dto);

            _context.RepairHistory.Add(entity);

            await _context.SaveChangesAsync();

            return RepairHistoryMapping.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool repairHistory = await _context.RepairHistory.AnyAsync(r => r.Id == id);

            if (!repairHistory)
                return false;

            RepairHistoryEntity? entity = await _context.RepairHistory.FindAsync(id);

            if (entity == null)
                return false;

            _context.RepairHistory.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(int id, UpdateRepairHistoryDto dto)
        {
            ValidationResult validation = RepairHistoryValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, validation.Errors));

            RepairHistoryEntity? entity = await _context.RepairHistory.FindAsync(id);

            if (entity == null)
                return false;

            RepairHistoryMapping.UpdateEntity(entity, dto);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
