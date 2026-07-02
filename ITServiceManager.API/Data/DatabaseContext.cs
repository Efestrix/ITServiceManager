using ITServiceManager.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) 
            : base(options)
        {
        }

        public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();
        public DbSet<DeviceEntity> Devices => Set<DeviceEntity>();
        public DbSet<DeviceTypeEntity> DeviceTypes => Set<DeviceTypeEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<RepairOrderEntity> RepairOrders => Set<RepairOrderEntity>();
        public DbSet<RepairHistoryEntity> RepairHistory => Set<RepairHistoryEntity>();
        public DbSet<RepairStatusEntity> RepairStatus => Set<RepairStatusEntity>();
        public DbSet<PhotoEntity> Photos => Set<PhotoEntity>();
    }
}
