using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vacation_API.Models
{
    public class LocalUser
    {
        public int Id { get; set; }
        public required string userName { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string Role { get; set; }
        
    }
}
