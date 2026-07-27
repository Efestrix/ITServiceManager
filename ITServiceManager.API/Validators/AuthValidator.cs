using ITServiceManager.API.Dtos.Authentication;

namespace ITServiceManager.API.Validators
{
    public static class AuthValidator
    {
        public static ValidationResult Validate(RegisterDto dto)
        {
            ValidationResult result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(dto.Username))
                result.Errors.Add("Username is required.");

            else if (dto.Username.Length < 3)
                result.Errors.Add("Username must be at least 3 characters long.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                result.Errors.Add("Email is required.");

            else if (!dto.Email.Contains("@"))
                result.Errors.Add("Email is not valid.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                result.Errors.Add("Password is required.");

            else
            {
                if (dto.Password.Length < 8)
                    result.Errors.Add("Password must contain at least 8 characters.");

                if (!dto.Password.Any(char.IsUpper))
                    result.Errors.Add("Password must contain at least one uppercase letter.");

                if (!dto.Password.Any(char.IsLower))
                    result.Errors.Add("Password must contain at least one lowercase letter.");

                if (!dto.Password.Any(char.IsDigit))
                    result.Errors.Add("Password must contain at least one number.");
            }

            if (dto.Role == null)
                result.Errors.Add("Role is required.");

            return result;
        }
        public static ValidationResult Validate(LoginDto dto)
        {
            ValidationResult result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(dto.Username))
                result.Errors.Add("Username is required.");

            else if (dto.Username.Length < 3)
                result.Errors.Add("Username must be at least 3 characters long.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                result.Errors.Add("Password is required.");

            else
            {
                if (dto.Password.Length < 8)
                    result.Errors.Add("Password must contain at least 8 characters.");

                if (!dto.Password.Any(char.IsUpper))
                    result.Errors.Add("Password must contain at least one uppercase letter.");

                if (!dto.Password.Any(char.IsLower))
                    result.Errors.Add("Password must contain at least one lowercase letter.");

                if (!dto.Password.Any(char.IsDigit))
                    result.Errors.Add("Password must contain at least one number.");
            }

            return result;
        }
    }
}
