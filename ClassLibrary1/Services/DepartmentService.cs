
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Entities;
using BLL.Mappers;
namespace BLL.Services;
public class DepartmentService : IDepartmentService
{
    private readonly IRepository<Department> _departments;
    private readonly IRepository<Worker> _workers;
    private readonly IRepository<Project> _projects;
    public DepartmentService(IRepository<Department> departments, IRepository<Worker> workers, IRepository<Project> projects)
    {
        _departments = departments; _workers = workers; _projects = projects;
    }
    public void AddDepartment(DepartmentModel d) { _departments.Add(d.ToEntity()); _departments.Save(); }
    public void UpdateDepartment(DepartmentModel d) { _departments.Update(d.ToEntity()); _departments.Save(); }
    public DepartmentModel? GetDepartment(int id) => _departments.GetById(id)?.ToModel();
    public IEnumerable<WorkerModel> GetWorkersInDepartment(string departmentName)
        => _workers.GetAll().Where(w => w.DepartmentName == departmentName).Select(w => w.ToModel());
    public IEnumerable<WorkerModel> GetWorkersInDepartmentSortedByPosition(string departmentName)
        => GetWorkersInDepartment(departmentName).OrderBy(w => w.PositionName);
    public IEnumerable<WorkerModel> GetWorkersInDepartmentSortedByProjectCost(string departmentName)
    {
        var projects = _projects.GetAll().ToList();
        return GetWorkersInDepartment(departmentName)
            .OrderByDescending(w => w.ProjectIds.Sum(id => projects.FirstOrDefault(p => p.Id == id)?.Cost ?? 0m));
    }
}
