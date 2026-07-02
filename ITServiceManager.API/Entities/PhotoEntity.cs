using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITServiceManager.API.Entities
{
    [Table("photos")]
    public class PhotoEntity
    {
        [Key]
        public int Id { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [ForeignKey(nameof(RepairOrder))]
        public int RepairOrderId { get; set; }

        public RepairOrderEntity RepairOrder { get; set; } = null!;

        public PhotoEntity() { }
    }
}
