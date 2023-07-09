using System.ComponentModel.DataAnnotations;

namespace Project_Managment_API.DTO
{
    public class UserSameTenantDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]

        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
       
    }
}
