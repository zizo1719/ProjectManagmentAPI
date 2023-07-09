using System.ComponentModel.DataAnnotations;

namespace Project_Managment_API.DTO
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
       
    }
}
