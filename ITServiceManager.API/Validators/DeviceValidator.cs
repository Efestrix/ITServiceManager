using ITServiceManager.API.Dtos.Device;

namespace ITServiceManager.API.Validators
{
    public static class DeviceValidator
    {
        public static ValidationResult Validate(CreateDeviceDto dto)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(dto.Manufacturer))
                result.AddError("Manufacturer is required.");

            if (dto.Manufacturer.Length > 50)
                result.AddError("Manufacturer cannot be longer than 50 characters.");

            if (string.IsNullOrWhiteSpace(dto.Model))
                result.AddError("Model is required.");

            if (dto.Model.Length > 100)
                result.AddError("Model cannot be longer than 100 characters.");

            if (string.IsNullOrWhiteSpace(dto.SerialNumber))
                result.AddError("Serial number is required.");

            if (dto.SerialNumber.Length > 100)
                result.AddError("Serial number cannot be longer than 100 characters.");

            if (dto.PurchaseDate > DateTime.Now)
                result.AddError("Purchase date cannot be in the future.");

            if (dto.CustomerId <= 0)
                result.AddError("Customer must be selected.");

            if (dto.DeviceTypeId <= 0)
                result.AddError("Device type must be selected.");

            return result;
        }
        public static ValidationResult Validate(UpdateDeviceDto dto)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(dto.Manufacturer))
                result.AddError("Manufacturer is required.");

            if (dto.Manufacturer.Length > 50)
                result.AddError("Manufacturer cannot be longer than 50 characters.");

            if (string.IsNullOrWhiteSpace(dto.Model))
                result.AddError("Model is required.");

            if (dto.Model.Length > 100)
                result.AddError("Model cannot be longer than 100 characters.");

            if (string.IsNullOrWhiteSpace(dto.SerialNumber))
                result.AddError("Serial number is required.");

            if (dto.SerialNumber.Length > 100)
                result.AddError("Serial number cannot be longer than 100 characters.");

            if (dto.PurchaseDate > DateTime.Now)
                result.AddError("Purchase date cannot be in the future.");

            if (dto.CustomerId <= 0)
                result.AddError("Customer must be selected.");

            if (dto.DeviceTypeId <= 0)
                result.AddError("Device type must be selected.");

            return result;
        }
    }
}
