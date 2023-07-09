using Project_Managment_API.Model;
using System.Collections.Generic;
using Task = Project_Managment_API.Model.Task;

namespace Project_Managment_API.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<Task> GetAllTasks();
        Task GetTaskById(string id);
        void AddTask(Task task);
        void UpdateTask(Task task);
        void DeleteTask(Task task);
        bool SaveChanges();
    }
}
