using Project_Managment_API._Attributes;

namespace Project_Managment_API.Model
{
    [MultiTenantQueryFilter(nameof(TenantId))]
    public class Comment
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string TaskId { get; set; }
        public Task Task { get; set; }
        public string TenantId { get; set; }
        public Tenant Tenant { get; set; }


    }
}
