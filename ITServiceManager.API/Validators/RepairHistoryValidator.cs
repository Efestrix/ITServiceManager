using ITServiceManager.API.Dtos.RepairHistory;

namespace ITServiceManager.API.Validators
{
    public class RepairHistoryValidator
    {
        public static ValidationResult Validate(CreateRepairHistoryDto dto)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(dto.Description))
                result.AddError("Description is required.");

            if (dto.Description.Length > 1000)
                result.AddError("Description cannot be longer than 1000 characters.");

            if (dto.CreatedAt > DateTime.Now)
                result.AddError("Created date cannot be in the future.");

            if (dto.HoursWorked < 0)
                result.AddError("Hours worked cannot be negative.");

            if (dto.RepairOrderId <= 0)
                result.AddError("Repair order must be selected.");

            return result;
        }

        public static ValidationResult Validate(UpdateRepairHistoryDto dto)
        {
            return Validate(new CreateRepairHistoryDto
            {
                Description = dto.Description,
                CreatedAt = dto.CreatedAt,
                HoursWorked = dto.HoursWorked,
                RepairOrderId = dto.RepairOrderId
            });
        }
    }
}
