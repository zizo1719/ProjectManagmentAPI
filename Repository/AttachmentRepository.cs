using Project_Management_API.Data;
using Project_Managment_API.Interfaces.Project_Managment_API.Repositories;
using Project_Managment_API.Model;

namespace Project_Managment_API.Repository
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly ProjectManagementDbContext _context;

        public AttachmentRepository(ProjectManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attachment> GetAllAttachments()
        {
            return _context.Attachments.ToList();
        }

        public Attachment GetAttachmentById(string id)
        {
            return _context.Attachments.FirstOrDefault(a => a.Id == id);
        }

        public void AddAttachment(Attachment attachment)
        {
            _context.Attachments.Add(attachment);
        }

        public void UpdateAttachment(Attachment attachment)
        {
            _context.Attachments.Update(attachment);
        }

        public void DeleteAttachment(Attachment attachment)
        {
            _context.Attachments.Remove(attachment);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
