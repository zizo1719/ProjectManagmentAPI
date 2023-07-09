using Project_Managment_API.Model;
using System.Collections.Generic;

namespace Project_Managment_API.Interfaces;

public interface IProjectRepository
{
    IEnumerable<Project> GetAllProjects();
    Project GetProjectById(string id);
    void AddProject(Project project);
    void UpdateProject(Project project);
    void DeleteProject(Project project);
    bool SaveChanges();
}
