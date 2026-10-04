using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.Customer;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Mappings;
using ITServiceManager.API.Middlewares;
using ITServiceManager.API.Services.Customer;
using ITServiceManager.API.Validators;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ITServiceManager.API.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        public CustomerService(DatabaseContext context)
            : base(context)
        {
        }
        public async Task<IEnumerable<CustomerDto>> GetAllAsync(
            CustomerQueryDto queryDTO)
        {
            IQueryable<CustomerEntity> customers = _context.Customers;

            if (!string.IsNullOrEmpty(queryDTO.firstName))
            {
                customers = customers.Where(
                    c => c.FirstName.Contains(queryDTO.firstName));
            }

            if (!string.IsNullOrWhiteSpace(queryDTO.lastName))
            {
                customers = customers.Where(
                    c => c.LastName.Contains(queryDTO.lastName));
            }

            if (!string.IsNullOrWhiteSpace(queryDTO.Email))
            {
                customers = customers.Where(
                    c => c.Email.Contains(queryDTO.Email));
            }

            List<CustomerEntity> result = await customers.ToListAsync();

            return result.Select(CustomerMapping.ToDto);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            bool customerExists = await _context.Customers.AnyAsync(c => c.Id == id);

            if (!customerExists)
                throw new NotFoundException("Customer does not exist.");

            CustomerEntity? customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                throw new NotFoundException("Customer does not exist.");

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
