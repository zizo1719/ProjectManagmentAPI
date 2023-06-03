using Microsoft.AspNetCore.Identity;

namespace Project_Managment_API.Model
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsComplete { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<TaskUser> TaskUsers { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}
