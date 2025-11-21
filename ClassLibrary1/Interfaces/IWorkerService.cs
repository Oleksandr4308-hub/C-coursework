using BLL.Models;
namespace BLL.Interfaces;
public interface IWorkerService
{
    void AddWorker(WorkerModel worker);
    void DeleteWorker(int id);
    void UpdateWorker(WorkerModel worker);
    WorkerModel? GetWorker(int id);
    IEnumerable<WorkerModel> GetAllWorkers();
    IEnumerable<WorkerModel> GetAllWorkersSortedByName();
    IEnumerable<WorkerModel> GetAllWorkersSortedByLastName();
    IEnumerable<WorkerModel> GetAllWorkersSortedByPositionSalary();
    IEnumerable<ProjectModel> GetWorkerProjects(int workerId);
    IEnumerable<WorkerModel> SearchWorkers(string keyword);
    IEnumerable<WorkerModel> AdvancedSearch(string lastName, string salaryAccountNumber);
}
