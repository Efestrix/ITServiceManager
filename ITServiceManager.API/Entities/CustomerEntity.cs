using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Numerics;

namespace ITServiceManager.API.Entities
{
    [Table("customers")]
    public class CustomerEntity
    {
        [Column("Id")]
        public int Id {  get; set; }
        [Column("FirstName")]
        public string FirstName { get; set; }
        [Column("LastName")]
        public string LastName { get; set; }
        [Column("Phone")]
        public string Phone { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Address")]
        public string Address { get; set; }
        public CustomerEntity()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
        }

        public CustomerEntity(string firstName, string lastName, string phone, string email, string address)
        {
            Id = 0;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Address = address;
        }
    }
}
