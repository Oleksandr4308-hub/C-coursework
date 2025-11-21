using DAL.Entities;
using BLL.Models;
namespace BLL.Mappers;
public static class EntityMapper
{
    public static WorkerModel ToModel(this Worker e) => new()
    {
        Id = e.Id,
        FirstName = e.FirstName,
        LastName = e.LastName,
        SalaryAccountNumber = e.SalaryAccountNumber,
        DepartmentName = e.DepartmentName,
        PositionName = e.PositionName,
        WorkExperienceYears = e.WorkExperienceYears,
        ProjectIds = new List<int>(e.ProjectIds)
    };
    public static Worker ToEntity(this WorkerModel m) => new()
    {
        Id = m.Id,
        FirstName = m.FirstName,
        LastName = m.LastName,
        SalaryAccountNumber = m.SalaryAccountNumber,
        DepartmentName = m.DepartmentName,
        PositionName = m.PositionName,
        WorkExperienceYears = m.WorkExperienceYears,
        ProjectIds = new List<int>(m.ProjectIds)
    };

    public static DepartmentModel ToModel(this Department e) => new() { Id = e.Id, Name = e.Name };
    public static Department ToEntity(this DepartmentModel m) => new() { Id = m.Id, Name = m.Name };

    public static PositionModel ToModel(this Position e) => new() { Id = e.Id, Title = e.Title, Salary = e.Salary, WorkHours = e.WorkHours };
    public static Position ToEntity(this PositionModel m) => new() { Id = m.Id, Title = m.Title, Salary = m.Salary, WorkHours = m.WorkHours };

    public static ProjectModel ToModel(this Project e) => new() { Id = e.Id, Name = e.Name, Cost = e.Cost };
    public static Project ToEntity(this ProjectModel m) => new() { Id = m.Id, Name = m.Name, Cost = m.Cost };
}
