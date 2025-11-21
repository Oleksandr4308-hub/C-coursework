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
    public class DepartmentServiceTests
    {
        private Mock<IRepository<Department>> _deptRepo = null!;
        private Mock<IRepository<Worker>> _workerRepo = null!;
        private Mock<IRepository<Project>> _projectRepo = null!;
        private DepartmentService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _deptRepo = new Mock<IRepository<Department>>();
            _workerRepo = new Mock<IRepository<Worker>>();
            _projectRepo = new Mock<IRepository<Project>>();

            _service = new DepartmentService(_deptRepo.Object, _workerRepo.Object, _projectRepo.Object);
        }

        [TestMethod]
        public void AddDepartment_ShouldCallAddAndSave()
        {
            var model = new DepartmentModel { Id = 1, Name = "IT" };

            _service.AddDepartment(model);

            _deptRepo.Verify(r => r.Add(It.IsAny<Department>()), Times.Once);
            _deptRepo.Verify(r => r.Save(), Times.Once);
        }

        [TestMethod]
        public void GetDepartment_ShouldReturnMappedModel()
        {
            _deptRepo.Setup(r => r.GetById(1)).Returns(new Department { Id = 1, Name = "IT" });

            var result = _service.GetDepartment(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("IT", result.Name);
        }

        [TestMethod]
        public void GetWorkersInDepartment_ShouldReturnCorrectWorkers()
        {
            _workerRepo.Setup(r => r.GetAll())
                .Returns(new List<Worker>
                {
                    new Worker{ Id=1, FirstName="A", DepartmentName="IT" },
                    new Worker{ Id=2, FirstName="B", DepartmentName="HR" }
                });

            var result = _service.GetWorkersInDepartment("IT").ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("A", result[0].FirstName);
        }
    }
}
