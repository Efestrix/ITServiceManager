using ITServiceManager.API.Dtos.DeviceType;

namespace ITServiceManager.API.Validators
{
    public static class DeviceTypeValidator
    {
        public static ValidationResult Validate(CreateDeviceTypeDto dto)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(dto.Name))
                result.AddError("Device type name is required.");

            if (dto.Name.Length > 50)
                result.AddError("Device type name cannot be longer than 50 characters.");

            return result;
        }

        public static ValidationResult Validate(UpdateDeviceTypeDto dto)
        {
            return Validate(new CreateDeviceTypeDto
            {
                Name = dto.Name
            });
        }
    }
}
