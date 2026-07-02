using ITServiceManager.API.Dtos.Customer;
using ITServiceManager.API.Entities;

namespace ITServiceManager.API.Services.Customer
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();

        Task<CustomerDto?> GetByIdAsync(int id);

        Task<CustomerDto> CreateAsync(CreateCustomerDto dto);

        Task<bool> UpdateAsync(int id, UpdateCustomerDto dto); 
        Task<bool> DeleteAsync(int id);
    }
}
