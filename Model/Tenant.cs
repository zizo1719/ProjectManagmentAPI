
using Project_Managment_API._Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Managment_API.Model
{
    public class Tenant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public ICollection<Comment> Comments { get; set; }



    }
}