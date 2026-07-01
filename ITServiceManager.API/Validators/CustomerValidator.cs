using ITServiceManager.API.Dtos.Customer;
using System.Text.RegularExpressions;

namespace ITServiceManager.API.Validators
{
    public static class CustomerValidator
    {
        public static ValidationResult Validate(CreateCustomerDto dto)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(dto.FirstName))
                result.AddError("First name is required.");

            if (string.IsNullOrWhiteSpace(dto.LastName))
                result.AddError("Last name is required.");

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                result.AddError("Email is required.");
            }
            else if (!Regex.IsMatch(dto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                result.AddError("Email is not valid.");
            }

            if (string.IsNullOrWhiteSpace(dto.Phone))
                result.AddError("Phone is required.");

            if (string.IsNullOrWhiteSpace(dto.Address))
                result.AddError("Address is required.");

            return result;
        }
        public static ValidationResult Validate(UpdateCustomerDto dto)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(dto.FirstName))
                result.AddError("First name is required.");

            if (string.IsNullOrWhiteSpace(dto.LastName))
                result.AddError("Last name is required.");

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                result.AddError("Email is required.");
            }
            else if (!Regex.IsMatch(dto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                result.AddError("Email is not valid.");
            }

            if (string.IsNullOrWhiteSpace(dto.Phone))
                result.AddError("Phone is required.");

            if (string.IsNullOrWhiteSpace(dto.Address))
                result.AddError("Address is required.");

            return result;
        }
    }
}
