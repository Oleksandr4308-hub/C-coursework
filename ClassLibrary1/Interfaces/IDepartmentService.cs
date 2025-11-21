using BLL.Models;
namespace BLL.Interfaces;
public interface IDepartmentService
{
    void AddDepartment(DepartmentModel d);
    void UpdateDepartment(DepartmentModel d);
    DepartmentModel? GetDepartment(int id);
    IEnumerable<WorkerModel> GetWorkersInDepartment(string departmentName);
    IEnumerable<WorkerModel> GetWorkersInDepartmentSortedByPosition(string departmentName);
    IEnumerable<WorkerModel> GetWorkersInDepartmentSortedByProjectCost(string departmentName);
}
