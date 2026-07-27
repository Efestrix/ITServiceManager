using ITServiceManager.API.Dtos.RepairStatus;

namespace ITServiceManager.API.Validators
{
    public static class RepairStatusValidator
    {
        public static ValidationResult Validate(CreateRepairStatusDto dto)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(dto.Name))
                result.AddError("Status name is required.");

            if (dto.Name.Length > 50)
                result.AddError("Status name cannot be longer than 50 characters.");

            return result;
        }

        public static ValidationResult Validate(UpdateRepairStatusDto dto)
        {
            return Validate(new CreateRepairStatusDto
            {
                Name = dto.Name
            });
        }
    }
}
