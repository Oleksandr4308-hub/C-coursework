
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Entities;
using BLL.Mappers;
namespace BLL.Services;
public class PositionService : IPositionService
{
    private readonly IRepository<Position> _positions;
    private readonly IRepository<Worker> _workers;
    private readonly IRepository<Project> _projects;
    public PositionService(IRepository<Position> positions, IRepository<Worker> workers, IRepository<Project> projects)
    {
        _positions = positions; _workers = workers; _projects = projects;
    }
    public void AddPosition(PositionModel p) { _positions.Add(p.ToEntity()); _positions.Save(); }
    public void UpdatePosition(PositionModel p) { _positions.Update(p.ToEntity()); _positions.Save(); }
    public PositionModel? GetPosition(int id) => _positions.GetById(id)?.ToModel();
    public IEnumerable<PositionModel> Top5AttractivePositions()
    {
        
        return _positions.GetAll()
            .OrderByDescending(p => p.WorkHours == 0 ? decimal.MaxValue : p.Salary / p.WorkHours)
            .Take(5)
            .Select(p => p.ToModel());
    }
    public WorkerModel? MostProfitableWorkerOnPosition(string positionName)
    {
        var workers = _workers.GetAll().Where(w => w.PositionName == positionName).ToList();
        if (!workers.Any()) return null;
        var projects = _projects.GetAll().ToList();
        Worker best = workers.OrderByDescending(w =>
        {
            var sum = w.ProjectIds.Sum(id => projects.FirstOrDefault(p => p.Id == id)?.Cost ?? 0m);
            var denom = w.WorkExperienceYears == 0 ? 1 : w.WorkExperienceYears;
            return (double)(sum / denom);
        }).First();
        return best.ToModel();
    }
}
