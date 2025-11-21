
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Entities;
using BLL.Mappers;
namespace BLL.Services;
public class WorkerService : IWorkerService
{
    private readonly IRepository<Worker> _workers;
    private readonly IRepository<Project> _projects;
    private readonly IRepository<Position> _positions;
    public WorkerService(IRepository<Worker> workers, IRepository<Project> projects, IRepository<Position> positions)
    {
        _workers = workers; _projects = projects; _positions = positions;
    }
    public void AddWorker(WorkerModel worker)
    {
        var e = worker.ToEntity();
        _workers.Add(e);
        _workers.Save();
    }
    public void DeleteWorker(int id) { _workers.Delete(id); _workers.Save(); }
    public void UpdateWorker(WorkerModel worker) { _workers.Update(worker.ToEntity()); _workers.Save(); }
    public WorkerModel? GetWorker(int id) => _workers.GetById(id)?.ToModel();
    public IEnumerable<WorkerModel> GetAllWorkers() => _workers.GetAll().Select(w => w.ToModel());
    public IEnumerable<WorkerModel> GetAllWorkersSortedByName() => GetAllWorkers().OrderBy(w => w.FirstName);
    public IEnumerable<WorkerModel> GetAllWorkersSortedByLastName() => GetAllWorkers().OrderBy(w => w.LastName);
    public IEnumerable<WorkerModel> GetAllWorkersSortedByPositionSalary()
    {
        var pos = _positions.GetAll().ToDictionary(p => p.Title, p => p.Salary);
        return GetAllWorkers().OrderByDescending(w => pos.ContainsKey(w.PositionName) ? pos[w.PositionName] : 0m);
    }
    public IEnumerable<ProjectModel> GetWorkerProjects(int workerId)
    {
        var w = _workers.GetById(workerId);
        if (w == null) return Enumerable.Empty<ProjectModel>();
        var projects = _projects.GetAll().Where(p => w.ProjectIds.Contains(p.Id)).Select(p => p.ToModel());
        return projects;
    }
    public IEnumerable<WorkerModel> SearchWorkers(string keyword)
    {
        keyword = keyword?.ToLower() ?? "";
        return GetAllWorkers().Where(w =>
            w.FirstName.ToLower().Contains(keyword) ||
            w.LastName.ToLower().Contains(keyword) ||
            w.SalaryAccountNumber.ToLower().Contains(keyword) ||
            w.DepartmentName.ToLower().Contains(keyword) ||
            w.PositionName.ToLower().Contains(keyword));
    }
    public IEnumerable<WorkerModel> AdvancedSearch(string lastName, string salaryAccountNumber)
    {
        return GetAllWorkers().Where(w => (string.IsNullOrEmpty(lastName) || w.LastName == lastName)
                                       && (string.IsNullOrEmpty(salaryAccountNumber) || w.SalaryAccountNumber == salaryAccountNumber));
    }
}
