using Project_Managment_API._Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Managment_API.Model
{
    [MultiTenantQueryFilter(nameof(TenantId))]
    public class Project
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }

        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public ICollection<Task> Tasks { get; set; }
        [ForeignKey("Tenant")]
        public string TenantId { get; set; }
        public Tenant Tenant { get; set; }

    }
}
