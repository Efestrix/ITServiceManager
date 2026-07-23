using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.RepairHistory;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Mappings;
using ITServiceManager.API.Middlewares;
using ITServiceManager.API.Services.Interfaces;
using ITServiceManager.API.Validators;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Services
{
    public class RepairHistoryService : BaseService, IRepairHistoryService
    {
        public RepairHistoryService(DatabaseContext context)
            : base(context)
        {
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

            await ValidateRepairOrder(dto.RepairOrderId);

            RepairHistoryEntity entity = RepairHistoryMapping.ToEntity(dto);

            _context.RepairHistory.Add(entity);

            await _context.SaveChangesAsync();

            return RepairHistoryMapping.ToDto(entity);
        }

        public async Task DeleteAsync(int id)
        {
            RepairHistoryEntity entity = await GetRepairHistoryAsync(id);

            _context.RepairHistory.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateRepairHistoryDto dto)
        {
            ValidationResult validation = RepairHistoryValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, validation.Errors));

            RepairHistoryEntity? entity = await _context.RepairHistory.FindAsync(id);

            await ValidateRepairOrder(dto.RepairOrderId);

            RepairHistoryMapping.UpdateEntity(entity, dto);

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Vrátí historii opravy nebo vyhodí NotFoundException.
        /// </summary>
        private async Task<RepairHistoryEntity> GetRepairHistoryAsync(int id)
        {
            RepairHistoryEntity? entity = await _context.RepairHistory.FindAsync(id);

            if (entity == null)
                throw new NotFoundException($"Repair history with id {id} was not found.");

            return entity;
        }

        /// <summary>
        /// Ověří existenci servisní zakázky.
        /// </summary>
        private async Task ValidateRepairOrder(int repairOrderId)
        {
            if (!await _context.RepairOrders.AnyAsync(r => r.Id == repairOrderId))
                throw new ValidationException("Repair order does not exist.");
        }
    }
}
