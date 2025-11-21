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
    public class PositionServiceTests
    {
        private Mock<IRepository<Position>> _posRepo = null!;
        private Mock<IRepository<Worker>> _workerRepo = null!;
        private Mock<IRepository<Project>> _projectRepo = null!;
        private PositionService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _posRepo = new Mock<IRepository<Position>>();
            _workerRepo = new Mock<IRepository<Worker>>();
            _projectRepo = new Mock<IRepository<Project>>();

            _service = new PositionService(_posRepo.Object, _workerRepo.Object, _projectRepo.Object);
        }

        [TestMethod]
        public void AddPosition_ShouldCallAddAndSave()
        {
            var model = new PositionModel { Id = 1, Title = "Dev" };

            _service.AddPosition(model);

            _posRepo.Verify(r => r.Add(It.IsAny<Position>()), Times.Once);
            _posRepo.Verify(r => r.Save(), Times.Once);
        }

        [TestMethod]
        public void Top5AttractivePositions_ShouldReturnCorrectOrder()
        {
            _posRepo.Setup(r => r.GetAll()).Returns(new List<Position>
            {
                new Position{ Id=1, Title="A", Salary=2000, WorkHours=20 },
                new Position{ Id=2, Title="B", Salary=3000, WorkHours=40 }
            });

            var result = _service.Top5AttractivePositions().ToList();

            Assert.AreEqual("A", result[0].Title);
        }

        [TestMethod]
        public void MostProfitableWorkerOnPosition_ShouldReturnCorrectWorker()
        {
            _workerRepo.Setup(r => r.GetAll())
                .Returns(new List<Worker>
                {
                    new Worker{ Id=1, PositionName="Dev", ProjectIds=new(){1}, WorkExperienceYears=2 },
                    new Worker{ Id=2, PositionName="Dev", ProjectIds=new(){1,2}, WorkExperienceYears=2 }
                });

            _projectRepo.Setup(r => r.GetAll())
                .Returns(new List<Project>
                {
                    new Project{ Id=1, Cost=1000 },
                    new Project{ Id=2, Cost=2000 }
                });

            var result = _service.MostProfitableWorkerOnPosition("Dev")!;

            Assert.AreEqual(2, result.Id);
        }
    }
}
