using Project_Management_API.Data;
using Project_Managment_API.Interfaces;
using Project_Managment_API.Model;
using System.Collections.Generic;
using System.Linq;

namespace Project_Managment_API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectManagementDbContext _context;

        public ProjectRepository(ProjectManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _context.Projects.ToList();
        }

        public Project GetProjectById(string id)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == id);
        }

        public void AddProject(Project project)
        {
            _context.Projects.Add(project);
        }

        public void UpdateProject(Project project)
        {
            _context.Projects.Update(project);
        }

        public void DeleteProject(Project project)
        {
            _context.Projects.Remove(project);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
