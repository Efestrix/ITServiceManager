using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITServiceManager.API.Entities
{
    [Table("devices")]
    public class DeviceEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Manufacturer { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        [Required]
        public string SerialNumber { get; set; } = string.Empty;

        public DateOnly PurchaseDate { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(DeviceType))]
        public int DeviceTypeId { get; set; }

        public CustomerEntity Customer { get; set; } = null!;

        public DeviceTypeEntity DeviceType { get; set; } = null!;

        public ICollection<RepairOrderEntity> RepairOrders { get; set; } = new List<RepairOrderEntity>();

        public DeviceEntity() { }
    }
}
