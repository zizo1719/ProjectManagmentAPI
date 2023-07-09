using Project_Managment_API._Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Managment_API.Model
{
    [MultiTenantQueryFilter(nameof(TenantId))]

    public class ChatMessage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }

        [ForeignKey("Sender")]
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        [ForeignKey("Receiver")]
        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }
        [ForeignKey("Tenant")]
        public string TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
