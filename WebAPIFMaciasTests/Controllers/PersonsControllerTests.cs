using NUnit.Framework;
using WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Microsoft.Extensions.Logging;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Tests
{
    [TestFixture()]
    public class PersonsControllerTests
    {
        [Test()]
        public void PersonsControllerTest()
        {
            Mock<ILogger<PersonsController>> mockLogger = new Mock<ILogger<PersonsController>>();
            Mock<IPersonsRepository> mockPersonsRepository = new Mock<IPersonsRepository>();
            Assert.IsInstanceOf<ControllerBase>(new PersonsController(mockLogger.Object, mockPersonsRepository.Object));
            Assert.IsInstanceOf<PersonsController>(new PersonsController(mockLogger.Object, mockPersonsRepository.Object));
        }

        [Test()]
        public void GetTest()
        {
            Mock<ILogger<PersonsController>> mockLogger = new Mock<ILogger<PersonsController>>();
            Mock<IPersonsRepository> mockPersonsRepository = new Mock<IPersonsRepository>();
            mockPersonsRepository.Setup(o => o.GetAll()).Returns(new List<Person>());
            PersonsController controller = new PersonsController(mockLogger.Object, mockPersonsRepository.Object);
            Assert.IsInstanceOf<IEnumerable<Person>>(controller.Get());
        }

        [Test()]
        public void GetPersonTest()
        {
            Mock<ILogger<PersonsController>> mockLogger = new Mock<ILogger<PersonsController>>();
            Mock<IPersonsRepository> mockPersonsRepository = new Mock<IPersonsRepository>();
            mockPersonsRepository.Setup(o => o.GetPerson(0)).Returns(new Person(0));
            PersonsController controller = new PersonsController(mockLogger.Object, mockPersonsRepository.Object);
            Assert.IsInstanceOf<Person>(controller.GetPerson(0));
        }

        [Test()]
        public void GetPersonsByColorTest()
        {
            Mock<ILogger<PersonsController>> mockLogger = new Mock<ILogger<PersonsController>>();
            Mock<IPersonsRepository> mockPersonsRepository = new Mock<IPersonsRepository>();
            mockPersonsRepository.Setup(o => o.GetPersonsByColor(1)).Returns(new List<Person>());
            PersonsController controller = new PersonsController(mockLogger.Object, mockPersonsRepository.Object);
            Assert.IsInstanceOf<IEnumerable<Person>>(controller.GetPersonsByColor(1));
        }
    }
}