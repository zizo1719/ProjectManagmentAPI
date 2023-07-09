using Project_Managment_API._Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Managment_API.Model
{
    
    public class ProjectUser
    {
        [ForeignKey("Project")]
        public string ProjectId { get; set; }
        public Project Project { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        
    }
}
