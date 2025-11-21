
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Entities;
using BLL.Mappers;
namespace BLL.Services;
public class ProjectService : IProjectService
{
    private readonly IRepository<Project> _projects;
    public ProjectService(IRepository<Project> projects) { _projects = projects; }
    public void AddProject(ProjectModel p) { _projects.Add(p.ToEntity()); _projects.Save(); }
    public ProjectModel? GetProject(int id) => _projects.GetById(id)?.ToModel();
    public IEnumerable<ProjectModel> SearchProjects(string keyword)
    {
        keyword = keyword?.ToLower() ?? "";
        return _projects.GetAll().Where(p => p.Name.ToLower().Contains(keyword)).Select(p => p.ToModel());
    }
}
