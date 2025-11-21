//// MSTestProject/Service/WorkerServiceTests.cs
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using DAL.Json;
//using DAL.Repositories;
//using DAL.Entities;
//using BLL.Services;
//using BLL.Interfaces;
//using System.Linq;

//[TestClass]
//public class WorkerServiceTests
//{
//    private string folder = "TestData";
//    private FileRepository<Worker> workerRepo;
//    private FileRepository<Project> projectRepo;
//    private FileRepository<DAL.Entities.Position> posRepo;
//    private IWorkerService service;

//    [TestInitialize]
//    public void Init()
//    {
//        System.IO.Directory.CreateDirectory(folder);
//        workerRepo = new FileRepository<Worker>(System.IO.Path.Combine(folder, "workers_test.json"), new JsonProvider<Worker>(), w => w.Id, (w, id) => w.Id = id);
//        projectRepo = new FileRepository<Project>(System.IO.Path.Combine(folder, "projects_test.json"), new JsonProvider<Project>(), p => p.Id, (p, id) => p.Id = id);
//        posRepo = new FileRepository<DAL.Entities.Position>(System.IO.Path.Combine(folder, "positions_test.json"), new JsonProvider<DAL.Entities.Position>(), p => p.Id, (p, id) => p.Id = id);
//        // clear
//        foreach (var f in new[] { "workers_test.json", "projects_test.json", "positions_test.json" })
//            if (System.IO.File.Exists(System.IO.Path.Combine(folder, f))) System.IO.File.Delete(System.IO.Path.Combine(folder, f));
//        service = new WorkerService(workerRepo, projectRepo, posRepo);
//    }

//    [TestMethod]
//    public void AddGetDeleteWorker()
//    {
//        var w = new BLL.Models.WorkerModel { FirstName = "A", LastName = "B", DepartmentName = "D", PositionName = "P", SalaryAccountNumber = "S1", WorkExperienceYears = 2 };
//        service.AddWorker(w);
//        var all = service.GetAllWorkers().ToList();
//        Assert.AreEqual(1, all.Count);
//        var got = service.GetWorker(all[0].Id);
//        Assert.IsNotNull(got);
//        service.DeleteWorker(got!.Id);
//        Assert.AreEqual(0, service.GetAllWorkers().Count());
//    }

//    [TestMethod]
//    public void UpdateWorkerTest()
//    {
//        var w = new BLL.Models.WorkerModel { FirstName = "AA", LastName = "BB" };
//        service.AddWorker(w);
//        var added = service.GetAllWorkers().First();
//        added.FirstName = "CC";
//        service.UpdateWorker(added);
//        var updated = service.GetWorker(added.Id);
//        Assert.AreEqual("CC", updated!.FirstName);
//    }

//    [TestMethod]
//    public void SearchAndSortTests()
//    {
//        service.AddWorker(new BLL.Models.WorkerModel { FirstName = "John", LastName = "Alpha" });
//        service.AddWorker(new BLL.Models.WorkerModel { FirstName = "Alice", LastName = "Beta" });
//        var byName = service.GetAllWorkersSortedByName().Select(w => w.FirstName).ToList();
//        CollectionAssert.AreEqual(new[] { "Alice", "John" }, byName.ToArray());
//        var search = service.SearchWorkers("john").FirstOrDefault();
//        Assert.IsNotNull(search);
//    }
//}
