using Microsoft.AspNetCore.Identity;

namespace Project_Managment_API.Model
{
    public class ApplicationUser : IdentityUser
    {
      
        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public ICollection<TaskUser> TaskUsers { get; set; }

    }
}
