using ITServiceManager.API.Dtos.User;

namespace ITServiceManager.API.Validators
{
    public static class UserValidator
    {
        public static ValidationResult Validate(CreateUserDto dto)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(dto.Username))
                result.AddError("Username is required.");

            if (dto.Username.Length < 3)
                result.AddError("Username must contain at least 3 characters.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                result.AddError("Password is required.");

            if (dto.Password.Length < 6)
                result.AddError("Password must contain at least 6 characters.");

            if (string.IsNullOrWhiteSpace(dto.FirstName))
                result.AddError("First name is required.");

            if (string.IsNullOrWhiteSpace(dto.LastName))
                result.AddError("Last name is required.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                result.AddError("Email is required.");

            if (!dto.Email.Contains("@"))
                result.AddError("Email is invalid.");

            return result;
        }

        public static ValidationResult Validate(UpdateUserDto dto)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(dto.Username))
                result.AddError("Username is required.");

            if (dto.Username.Length < 3)
                result.AddError("Username must contain at least 3 characters.");

            if (string.IsNullOrWhiteSpace(dto.FirstName))
                result.AddError("First name is required.");

            if (string.IsNullOrWhiteSpace(dto.LastName))
                result.AddError("Last name is required.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                result.AddError("Email is required.");

            if (!dto.Email.Contains("@"))
                result.AddError("Email is invalid.");

            return result;
        }
    }
}
