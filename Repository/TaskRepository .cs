using Project_Management_API.Data;
using Project_Managment_API.Model;
using System.Collections.Generic;
using System.Linq;
using Task = Project_Managment_API.Model.Task;

namespace Project_Managment_API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ProjectManagementDbContext _context;

        public TaskRepository(ProjectManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Task> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        public Task GetTaskById(string id)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public void AddTask(Task task)
        {
            _context.Tasks.Add(task);
        }

        public void UpdateTask(Task task)
        {
            _context.Tasks.Update(task);
        }

        public void DeleteTask(Task task)
        {
            _context.Tasks.Remove(task);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
