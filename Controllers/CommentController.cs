using Microsoft.AspNetCore.Mvc;
using Project_Managment_API.Interfaces;
using Project_Managment_API.Model;
using Project_Managment_API.Repositories;

namespace Project_Managment_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _repository;

        public CommentsController(ICommentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllComments()
        {
            var comments = _repository.GetAllComments();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public IActionResult GetCommentById(string id)
        {
            var comment = _repository.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            _repository.AddComment(comment);
            _repository.SaveChanges();

            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
        }

      
        [HttpPut("{id}")]
        public IActionResult UpdateComment(string id, Comment comment)
        {
            var existingComment = _repository.GetCommentById(id);

            if (existingComment == null)
            {
                return NotFound();
            }

            existingComment.Text = comment.Text;
            existingComment.Date = comment.Date;
            existingComment.TaskId = comment.TaskId;
            existingComment.Task = comment.Task;
            existingComment.TenantId = comment.TenantId;
            existingComment.Tenant = comment.Tenant;

            _repository.UpdateComment(existingComment);
            _repository.SaveChanges();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(string id)
        {
            var comment = _repository.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            _repository.DeleteComment(comment);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
