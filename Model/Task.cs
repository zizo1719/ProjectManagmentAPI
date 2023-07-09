using Microsoft.AspNetCore.Identity;

using Project_Managment_API._Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Managment_API.Model
{
    [MultiTenantQueryFilter(nameof(TenantId))]
    public class Task
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsComplete { get; set; }
        public string ProjectId { get; set; }
        public Project Project { get; set; }
        [ForeignKey("Tenant")]

        public string TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<TaskUser> TaskUsers { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        
    }

    }
