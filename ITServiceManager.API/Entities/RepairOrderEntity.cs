using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITServiceManager.API.Entities
{
    [Table("repairorders")]
    public class RepairOrderEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OrderNumber { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        [ForeignKey(nameof(Device))]
        public int DeviceId { get; set; }

        [ForeignKey(nameof(Technician))]
        public int TechnicianId { get; set; }

        [ForeignKey(nameof(Status))]
        public int StatusId { get; set; }

        public DeviceEntity Device { get; set; } = null!;

        public UserEntity Technician { get; set; } = null!;

        public RepairStatusEntity Status { get; set; } = null!;

        public ICollection<RepairHistoryEntity> History { get; set; } = new List<RepairHistoryEntity>();

        public ICollection<PhotoEntity> Photos { get; set; } = new List<PhotoEntity>();

        public RepairOrderEntity() { }
    }
}
