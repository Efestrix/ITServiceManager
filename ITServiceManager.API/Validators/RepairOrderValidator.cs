using ITServiceManager.API.Dtos.RepairOrder;

namespace ITServiceManager.API.Validators
{
    public class RepairOrderValidator
    {
        public static ValidationResult Validate(CreateRepairOrderDto dto)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(dto.OrderNumber))
                result.AddError("Order number is required.");

            if (dto.OrderNumber.Length > 20)
                result.AddError("Order number cannot be longer than 20 characters.");

            if (string.IsNullOrWhiteSpace(dto.Description))
                result.AddError("Description is required.");

            if (dto.Description.Length > 1000)
                result.AddError("Description is too long.");

            if (dto.Price < 0)
                result.AddError("Price cannot be negative.");

            if (dto.CreatedAt > DateTime.Now)
                result.AddError("Created date cannot be in the future.");

            if (dto.FinishedAt.HasValue &&
                dto.FinishedAt < dto.CreatedAt)
            {
                result.AddError("Finished date cannot be earlier than created date.");
            }

            if (dto.DeviceId <= 0)
                result.AddError("Device must be selected.");

            if (dto.TechnicianId <= 0)
                result.AddError("Technician must be selected.");

            if (dto.StatusId <= 0)
                result.AddError("Status must be selected.");

            return result;
        }
        public static ValidationResult Validate(UpdateRepairOrderDto dto)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(dto.OrderNumber))
                result.AddError("Order number is required.");

            if (dto.OrderNumber.Length > 20)
                result.AddError("Order number cannot be longer than 20 characters.");

            if (string.IsNullOrWhiteSpace(dto.Description))
                result.AddError("Description is required.");

            if (dto.Description.Length > 1000)
                result.AddError("Description is too long.");

            if (dto.Price < 0)
                result.AddError("Price cannot be negative.");

            if (dto.CreatedAt > DateTime.Now)
                result.AddError("Created date cannot be in the future.");

            if (dto.FinishedAt.HasValue &&
                dto.FinishedAt < dto.CreatedAt)
            {
                result.AddError("Finished date cannot be earlier than created date.");
            }

            if (dto.DeviceId <= 0)
                result.AddError("Device must be selected.");

            if (dto.TechnicianId <= 0)
                result.AddError("Technician must be selected.");

            if (dto.StatusId <= 0)
                result.AddError("Status must be selected.");

            return result;
        }
    }
}
