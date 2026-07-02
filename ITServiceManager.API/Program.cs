
using ITServiceManager.API.Data;
using ITServiceManager.API.Services;
using ITServiceManager.API.Services.Customer;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

                options.UseMySql(
                    connection,
                    ServerVersion.AutoDetect(connection));
            });

            builder.Services.AddScoped<ICustomerService, CustomerService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
