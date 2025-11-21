using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BLL.Services;
using DAL.Interfaces;
using DAL.Entities;
using BLL.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Tests
{
    [TestClass]
    public class WorkerServiceTests
    {
        private Mock<IRepository<Worker>> _workerRepo = null!;
        private Mock<IRepository<Project>> _projectRepo = null!;
        private Mock<IRepository<Position>> _positionRepo = null!;
        private WorkerService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _workerRepo = new Mock<IRepository<Worker>>();
            _projectRepo = new Mock<IRepository<Project>>();
            _positionRepo = new Mock<IRepository<Position>>();

            _service = new WorkerService(_workerRepo.Object, _projectRepo.Object, _positionRepo.Object);
        }

        [TestMethod]
        public void AddWorker_ShouldCallAddAndSave()
        {
            var model = new WorkerModel { Id = 1, FirstName = "Test" };

            _service.AddWorker(model);

            _workerRepo.Verify(r => r.Add(It.IsAny<Worker>()), Times.Once);
            _workerRepo.Verify(r => r.Save(), Times.Once);
        }

        [TestMethod]
        public void GetWorker_ShouldReturnCorrectModel()
        {
            _workerRepo.Setup(r => r.GetById(1))
                .Returns(new Worker { Id = 1, FirstName = "John" });

            var result = _service.GetWorker(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("John", result.FirstName);
        }

        [TestMethod]
        public void SearchWorkers_ShouldFindByKeyword()
        {
            _workerRepo.Setup(r => r.GetAll())
                .Returns(new List<Worker>
                {
                    new Worker{ Id=1, FirstName="Bob" },
                    new Worker{ Id=2, FirstName="Alice" }
                });

            var result = _service.SearchWorkers("bo").ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Bob", result[0].FirstName);
        }
    }
}
