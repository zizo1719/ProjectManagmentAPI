using Microsoft.AspNetCore.Identity;
namespace Project_Managment_API.Model
{
    public class TaskUser
    {
        public int TaskId { get; set; }
        public Task Task { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
