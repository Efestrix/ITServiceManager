using ITServiceManager.API.Data;
using ITServiceManager.API.Middlewares;
using ITServiceManager.API.Validators;

namespace ITServiceManager.API.Services
{
    public abstract class BaseService
    {
        protected readonly DatabaseContext _context;

        protected BaseService(DatabaseContext context)
        {
            _context = context;
        }

        protected static void ThrowIfInvalid(ValidationResult validation)
        {
            if (!validation.IsValid)
                throw new ValidationException(string.Join(Environment.NewLine, validation.Errors));
        }
    }
}
