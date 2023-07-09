using Microsoft.AspNetCore.Mvc;
using Project_Managment_API.Interfaces;
using Project_Managment_API.Model;
using Project_Managment_API.Repositories;

namespace Project_Managment_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _repository;

        public ProjectsController(IProjectRepository repository)
        {
            _repository = repository;
        }

       
        [HttpGet]
        public IActionResult GetAllProjects()
        {
            var projects = _repository.GetAllProjects();
            return Ok(projects);
        }

      
        [HttpGet("{id}")]
        public IActionResult GetProjectById(string id)
        {
            var project = _repository.GetProjectById(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

     
        [HttpPost]
        public IActionResult AddProject(Project project)
        {
            _repository.AddProject(project);
            _repository.SaveChanges();

            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }

        // PUT: api/projects/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProject(string id, Project project)
        {
            var existingProject = _repository.GetProjectById(id);

            if (existingProject == null)
            {
                return NotFound();
            }

            existingProject.Title = project.Title;
            existingProject.Description = project.Description;
            existingProject.StartDate = project.StartDate;
            existingProject.EndDate = project.EndDate;
            existingProject.Status = project.Status;
            existingProject.TenantId = project.TenantId;
            existingProject.Tenant = project.Tenant;

            _repository.UpdateProject(existingProject);
            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE: api/projects/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(string id)
        {
            var project = _repository.GetProjectById(id);

            if (project == null)
            {
                return NotFound();
            }

            _repository.DeleteProject(project);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
