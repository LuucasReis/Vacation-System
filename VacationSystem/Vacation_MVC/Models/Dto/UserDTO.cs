namespace Vacation_MVC.Models.Dto
{
    public class UserDTO
    {
        public int Id { get; set; }
        public required string userName { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string Role { get; set; }
    }
}
