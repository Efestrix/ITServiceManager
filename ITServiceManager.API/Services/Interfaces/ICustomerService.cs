using ITServiceManager.API.Dtos.Customer;
using ITServiceManager.API.Entities;

namespace ITServiceManager.API.Services.Customer
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();

        Task<CustomerDto?> GetByIdAsync(int id);

        Task<CustomerDto> CreateAsync(CreateCustomerDto dto);

        Task UpdateAsync(int id, UpdateCustomerDto dto); 
        Task DeleteAsync(int id);
    }
}
