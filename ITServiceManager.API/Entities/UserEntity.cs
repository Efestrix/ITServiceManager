using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ITServiceManager.API.Entities
{
    [Table("users")]
    public class UserEntity
    {
        [Key]
        [Column("Id")]
        public int Id {  get; set; }

        [Required]
        [Column("Username")]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [Column("FirstName")]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Column("LastName")]
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }

        public ICollection<RepairOrderEntity> RepairOrders { get; set; } = new List<RepairOrderEntity>();


        public UserEntity() { }
    }
}
