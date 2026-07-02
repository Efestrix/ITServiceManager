using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITServiceManager.API.Entities
{
    [Table("repairhistory")]
    public class RepairHistoryEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal HoursWorked { get; set; }

        [ForeignKey(nameof(RepairOrder))]
        public int RepairOrderId { get; set; }

        public RepairOrderEntity RepairOrder { get; set; } = null!;

        public RepairHistoryEntity() { }
    }
}
