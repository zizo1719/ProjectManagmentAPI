using Microsoft.AspNetCore.Mvc;
using Project_Managment_API.Interfaces.Project_Managment_API.Repositories;
using Project_Managment_API.Model;

namespace Project_Managment_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentRepository _repository;

        public AttachmentsController(IAttachmentRepository repository)
        {
            _repository = repository;
        }

        
        [HttpGet]
        public IActionResult GetAllAttachments()
        {
            var attachments = _repository.GetAllAttachments();
            return Ok(attachments);
        }

      
        [HttpGet("{id}")]
        public IActionResult GetAttachmentById(string id)
        {
            var attachment = _repository.GetAttachmentById(id);

            if (attachment == null)
            {
                return NotFound();
            }

            return Ok(attachment);
        }

        [HttpPost]
        public IActionResult AddAttachment(Attachment attachment)
        {
            _repository.AddAttachment(attachment);
            _repository.SaveChanges();

            return CreatedAtAction(nameof(GetAttachmentById), new { id = attachment.Id }, attachment);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAttachment(string id, Attachment attachment)
        {
            var existingAttachment = _repository.GetAttachmentById(id);

            if (existingAttachment == null)
            {
                return NotFound();
            }

            existingAttachment.FileName = attachment.FileName;
            existingAttachment.FileType = attachment.FileType;
            existingAttachment.FileSize = attachment.FileSize;
            existingAttachment.TaskId = attachment.TaskId;
            existingAttachment.Task = attachment.Task;
            existingAttachment.TenantId = attachment.TenantId;
            existingAttachment.Tenant = attachment.Tenant;

            _repository.UpdateAttachment(existingAttachment);
            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE: api/attachments/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAttachment(string id)
        {
            var attachment = _repository.GetAttachmentById(id);

            if (attachment == null)
            {
                return NotFound();
            }

            _repository.DeleteAttachment(attachment);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
