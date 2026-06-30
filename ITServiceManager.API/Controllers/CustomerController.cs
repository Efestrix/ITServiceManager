using ITServiceManager.API.Data;
using ITServiceManager.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DatabaseContext database;

        public CustomerController(DatabaseContext database)
        {
            this.database = database;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            List<CustomerEntity> customers = await database.Customers.ToListAsync();

            return Ok(customers);
        }
    }
}
