using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITServiceManager.API.Entities
{
    [Table("repairstatus")]
    public class RepairStatusEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<RepairOrderEntity> RepairOrders { get; set; } = new List<RepairOrderEntity>();

        public RepairStatusEntity() { }
    }
}
