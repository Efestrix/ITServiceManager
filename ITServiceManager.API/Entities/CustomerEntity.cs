using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Numerics;

namespace ITServiceManager.API.Entities
{
    [Table("customers")]
    public class CustomerEntity
    {
        [Key]
        [Column("Id")]
        public int Id {  get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public ICollection<DeviceEntity> Devices { get; set; } = new List<DeviceEntity>();

        public CustomerEntity() { }
    }
}
