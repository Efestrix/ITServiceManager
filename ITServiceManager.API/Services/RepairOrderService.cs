using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.RepairOrder;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Mappings;
using ITServiceManager.API.Services.Interfaces;
using ITServiceManager.API.Validators;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Services
{
    public class RepairOrderService : IRepairOrderService
    {
        private readonly DatabaseContext _context;

        public RepairOrderService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RepairOrderDto>> GetAllAsync()
        {
            List<RepairOrderEntity> repairOrders = await _context.RepairOrders.ToListAsync();

            return repairOrders
                .Select(RepairOrderMapping.ToDto)
                .ToList();
        }

        public async Task<RepairOrderDto?> GetByIdAsync(int id)
        {
            bool repairOrderExists = await _context.RepairOrders.AnyAsync(c => c.Id == id);

            if (!repairOrderExists)
                return null;

            RepairOrderEntity? entity = await _context.RepairOrders.FindAsync(id);

            if (entity == null)
                return null;

            return RepairOrderMapping.ToDto(entity);
        }
        public async Task<RepairOrderDto> CreateAsync(CreateRepairOrderDto dto)
        {
            ValidationResult validation = RepairOrderValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, validation.Errors));

            RepairOrderEntity entity = RepairOrderMapping.ToEntity(dto);

            ValidateRepairOrder(entity);

            _context.RepairOrders.Add(entity);

            await _context.SaveChangesAsync();

            return RepairOrderMapping.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool repairOrderExists = await _context.Devices.AnyAsync(c => c.Id == id);

            if (!repairOrderExists)
                return false;

            RepairOrderEntity? entity = await _context.RepairOrders.FindAsync(id);

            if (entity == null)
                return false;

            _context.RepairOrders.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> UpdateAsync(int id, UpdateRepairOrderDto dto)
        {
            bool repairOrderExists = await _context.Devices.AnyAsync(c => c.Id == id);

            if (!repairOrderExists)
                return false;

            ValidationResult validation = RepairOrderValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, validation.Errors));

            RepairOrderEntity? repairOrder = await _context.RepairOrders.FindAsync(id);

            if (repairOrder == null)
                return false;

            ValidateRepairOrder(repairOrder);

            RepairOrderMapping.UpdateEntity(repairOrder, dto);

            await _context.SaveChangesAsync();

            return true;
        }
        private void ValidateRepairOrder(RepairOrderEntity entity)
        {
            if (!_context.Devices.Any(c => c.Id == entity.DeviceId))
                throw new Exception("Device does not exist.");

            if (!_context.Users.Any(x =>
            x.Id == entity.TechnicianId &&
            x.Role == UserRole.Technician))
                throw new Exception("Selected user is not a technician.");

            if (!_context.RepairStatus.Any(x =>
            x.Id == entity.StatusId))
                throw new Exception("Repair status does not exist.");
        }
    }
}
