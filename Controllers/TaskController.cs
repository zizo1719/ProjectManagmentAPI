using Microsoft.AspNetCore.Mvc;
using Project_Managment_API.Model;
using Project_Managment_API.Repositories;
using Task = Project_Managment_API.Model.Task;

namespace Project_Managment_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;

        public TasksController(ITaskRepository repository)
        {
            _repository = repository;
        }

        // GET: api/tasks
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var tasks = _repository.GetAllTasks();
            return Ok(tasks);
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public IActionResult GetTaskById(string id)
        {
            var task = _repository.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public IActionResult AddTask(Task task)
        {
            _repository.AddTask(task);
            _repository.SaveChanges();

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTask(string id, Task task)
        {
            var existingTask = _repository.GetTaskById(id);

            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.DueDate = task.DueDate;
            existingTask.IsComplete = task.IsComplete;
            existingTask.ProjectId = task.ProjectId;
            existingTask.Project = task.Project;
            existingTask.TenantId = task.TenantId;
            existingTask.Tenant = task.Tenant;

            _repository.UpdateTask(existingTask);
            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(string id)
        {
            var task = _repository.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            _repository.DeleteTask(task);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
