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
    public class ProjectServiceTests
    {
        private Mock<IRepository<Project>> _projectsRepoMock = null!;
        private ProjectService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _projectsRepoMock = new Mock<IRepository<Project>>();
            _service = new ProjectService(_projectsRepoMock.Object);
        }

        [TestMethod]
        public void AddProject_ShouldCallAddAndSave()
        {
         
            var model = new ProjectModel { Id = 1, Name = "Test", Cost = 5000 };

            
            _service.AddProject(model);

            _projectsRepoMock.Verify(r => r.Add(It.IsAny<Project>()), Times.Once);
            _projectsRepoMock.Verify(r => r.Save(), Times.Once);
        }

        [TestMethod]
        public void GetProject_ShouldReturnMappedModel()
        {
            
            _projectsRepoMock.Setup(r => r.GetById(1)).Returns(
                new Project { Id = 1, Name = "Alpha", Cost = 3000 });

            
            var result = _service.GetProject(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Alpha", result.Name);
        }

        [TestMethod]
        public void SearchProjects_ShouldReturnFilteredResults()
        {
            
            _projectsRepoMock.Setup(r => r.GetAll())
                .Returns(new List<Project>
                {
                    new Project{ Id=1, Name="Alpha Design" },
                    new Project{ Id=2, Name="Beta Work" },
                    new Project{ Id=3, Name="Gamma" }
                });

            
            var results = _service.SearchProjects("a").ToList();

            
            Assert.AreEqual(3, results.Count);
        }
    }
}
