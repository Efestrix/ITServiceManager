using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.Customer;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Mappings;
using ITServiceManager.API.Middlewares;
using ITServiceManager.API.Services.Customer;
using ITServiceManager.API.Validators;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        public CustomerService(DatabaseContext context)
            : base(context)
        {
        }
        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            List<CustomerEntity> customers = await _context.Customers.ToListAsync();

            return customers
                .Select(CustomerMapping.ToDto)
                .ToList();
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            bool customerExists = await _context.Customers.AnyAsync(c => c.Id == id);

            if (!customerExists)
                return null;

            CustomerEntity? customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return null;

            return CustomerMapping.ToDto(customer);
        }
        public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
        {
            ValidationResult validation = CustomerValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, validation.Errors));

            CustomerEntity customer = CustomerMapping.ToEntity(dto);

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return CustomerMapping.ToDto(customer);
        }
        public async Task UpdateAsync(int id, UpdateCustomerDto dto)
        {
            bool customerExists = await _context.Customers.AnyAsync(c => c.Id == id);

            if (!customerExists)
                throw new NotFoundException("Not Found");

            CustomerEntity? entity = await _context.Customers.FindAsync(id);

            if (entity == null)
                throw new NotFoundException("Not Found");

            ValidationResult validation = CustomerValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, validation.Errors));

            CustomerMapping.UpdateEntity(entity, dto);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            bool customerExists = await _context.Customers.AnyAsync(c => c.Id == id);

            if (!customerExists)
                throw new NotFoundException("Not Found");

            CustomerEntity? entity = await _context.Customers.FindAsync(id);

            if (entity == null)
                throw new NotFoundException("Not Found");

            _context.Customers.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}
