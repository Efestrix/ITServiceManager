using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ITServiceManager.API.Entities
{
    [Table("users")]
    public class UserEntity
    {
        [Column("Id")]
        public int Id {  get; set; }
        [Column("Username")]
        public string Username {  get; set; }
        [Column("FirstName")]
        public string FirstName {  get; set; }
        [Column("LastName")]
        public string LastName {  get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Role")]
        public RoleEntity Role { get; set; }

        public UserEntity()
        {
            Id = 0;
            Username = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Role = null!;
        }

        public UserEntity(string username, string firstName, string lastName, string email, RoleEntity role)
        {
            Id = 0;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Role = role;
        }
    }
}
