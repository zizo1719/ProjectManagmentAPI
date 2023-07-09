using Project_Managment_API.Model;

namespace Project_Managment_API.Interfaces
{

    namespace Project_Managment_API.Repositories
    {
        public interface IAttachmentRepository
        {
            IEnumerable<Attachment> GetAllAttachments();
            Attachment GetAttachmentById(string id);
            void AddAttachment(Attachment attachment);
            void UpdateAttachment(Attachment attachment);
            void DeleteAttachment(Attachment attachment);
            bool SaveChanges();
        }
    }

}
