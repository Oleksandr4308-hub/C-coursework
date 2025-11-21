using DAL.Json;
using DAL.Repositories;
using DAL.Interfaces;
using DAL.Entities;
using BLL.Services;
using BLL.Interfaces;
namespace BLL.Dependency;
public static class ServiceFactory
{
    
    
    public static (IWorkerService workerService, IDepartmentService deptService, IPositionService posService, IProjectService projService)
        CreateServices(string storageFolder)
    {
        var workerRepo = new FileRepository<Worker>(System.IO.Path.Combine(storageFolder, "workers.json"), new JsonProvider<Worker>(),
            w => w.Id, (w, id) => w.Id = id);  
        var deptRepo = new FileRepository<Department>(System.IO.Path.Combine(storageFolder, "departments.json"), new JsonProvider<Department>(),
            d => d.Id, (d, id) => d.Id = id);
        var posRepo = new FileRepository<Position>(System.IO.Path.Combine(storageFolder, "positions.json"), new JsonProvider<Position>(),
            p => p.Id, (p, id) => p.Id = id);
        var projRepo = new FileRepository<Project>(System.IO.Path.Combine(storageFolder, "projects.json"), new JsonProvider<Project>(),
            p => p.Id, (p, id) => p.Id = id);

        var workerService = new WorkerService(workerRepo, projRepo, posRepo);
        var deptService = new DepartmentService(deptRepo, workerRepo, projRepo);
        var posService = new PositionService(posRepo, workerRepo, projRepo);
        var projService = new ProjectService(projRepo);
        return (workerService, deptService, posService, projService);
    }
}
