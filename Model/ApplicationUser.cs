using Microsoft.AspNetCore.Identity;
using Project_Managment_API._Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Managment_API.Model
{
    [MultiTenantQueryFilter(nameof(TenantId))]
    public class ApplicationUser : IdentityUser
    {

        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public ICollection<TaskUser> TaskUsers { get; set; }
        public OtpModel Otp { get; set; }

        [ForeignKey("Tenant")]
        public string TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<ChatMessage> SentMessages { get; set; }
        public ICollection<ChatMessage> ReceivedMessages { get; set; }



    }
}
