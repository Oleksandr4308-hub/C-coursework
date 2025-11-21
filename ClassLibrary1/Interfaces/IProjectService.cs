using BLL.Models;
namespace BLL.Interfaces;
public interface IProjectService
{
    void AddProject(ProjectModel p);
    ProjectModel? GetProject(int id);
    IEnumerable<ProjectModel> SearchProjects(string keyword);
}
