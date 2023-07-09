using Microsoft.AspNetCore.Identity;

using Project_Managment_API._Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Managment_API.Model
{
    public class TaskUser
    {
        [ForeignKey("Task")]
        public string TaskId { get; set; }
        public Task Task { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
