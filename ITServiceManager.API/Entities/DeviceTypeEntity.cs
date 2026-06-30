using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace ITServiceManager.API.Entities
{
    [Table("devicetypes")]
    public class DeviceTypeEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<DeviceEntity> Devices { get; set; } = new List<DeviceEntity>();

        public DeviceTypeEntity() { }
    }
}
