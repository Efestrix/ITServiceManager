using ITServiceManager.API.Dtos.Customer;
using ITServiceManager.API.Entities;

namespace ITServiceManager.API.Mappings
{
    public static class CustomerMapping
    {
        public static CustomerDto ToDto(CustomerEntity entity)
        {
            return new CustomerDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Phone = entity.Phone,
                Address = entity.Address
            };
        }

        public static CustomerEntity ToEntity(CreateCustomerDto dto)
        {
            return new CustomerEntity
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };
        }

        public static void UpdateEntity(CustomerEntity entity, UpdateCustomerDto dto)
        {
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;
            entity.Address = dto.Address;
        }
    }
}
