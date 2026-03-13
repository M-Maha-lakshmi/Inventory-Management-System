using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
