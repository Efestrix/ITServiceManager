using ITServiceManager.API.Data;
using ITServiceManager.API.Middlewares;
using ITServiceManager.API.Services;
using ITServiceManager.API.Services.Customer;
using ITServiceManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

namespace ITServiceManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /* JWT Authentication */

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;

                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],

                    ValidAudience = builder.Configuration["Jwt:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]
                            ?? throw new InvalidOperationException(
                                "JWT key is missing from configuration.")))
                };
            });

            /* Database */

            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                string? connection = 
                    builder.Configuration.GetConnectionString(
                        "DefaultConnection")
                    ?? throw new InvalidOperationException(
                        "Database connection string is missing.");

                options.UseMySql(
                    connection,
                    ServerVersion.AutoDetect(connection));
            });

            builder.Services.AddAuthorization();

            /* Dependency Injection */

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IDeviceService, DeviceService>();
            builder.Services.AddScoped<IDeviceTypeService, DeviceTypeService>();
            builder.Services.AddScoped<IPhotoService, PhotoService>();
            builder.Services.AddScoped<IRepairHistoryService, RepairHistoryService>();
            builder.Services.AddScoped<IRepairOrderService, RepairOrderService>();
            builder.Services.AddScoped<IRepairStatusService, RepairStatusService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "IT Service Manager API",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", 
                    new OpenApiSecurityScheme
                {
                    Description = "Enter JWT token.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIs...",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(document => 
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecuritySchemeReference(
                                "Bearer",
                                document)
                        ] = []
                    });
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(
                        "/swagger/v1/swagger.json",
                        "ITServiceManager API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
