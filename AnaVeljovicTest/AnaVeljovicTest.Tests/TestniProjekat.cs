using AnaVeljovicTest.Controllers;
using AnaVeljovicTest.Interface;
using AnaVeljovicTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace AnaVeljovicTest.Tests
{
    [TestClass]
    class TestniProjekat
    {
        [TestMethod]
        public void GetReturnsNekretnineWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<INekretnina>();
            mockRepository.Setup(x => x.GetById(20)).Returns(new Nekretnina { Id = 20 });

            var controller = new NekretnineController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(20);
            var contentResult = actionResult as OkNegotiatedContentResult<Nekretnina>;
            //ok ja ocekujem  da jem


            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(20, contentResult.Content.Id);
        }
        [TestMethod]
        public void DeleteReturnsNotFound()////// DELETE 
        {
            // Arrange 
            var mockRepository = new Mock<INekretnina>();
            var controller = new NekretnineController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<INekretnina>();
            var controller = new NekretnineController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(10, new Nekretnina { Id = 9, Mesto = "Novi Sad", Oznaka = "Nek02", GodinaIzgradnje = 1974, Kvadratura = 100, Cena = 40000, AgentId = 1 });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }
        [TestMethod]
        public void GetReturnsMultipleObjects()
        {
            //drugi case toga 

            // Arrange
            List<Nekretnina> nekretnine = new List<Nekretnina>();
            nekretnine.Add(new Nekretnina { Id = 1, Mesto = "Novi Sad", Oznaka = "Nek02", GodinaIzgradnje = 1974, Kvadratura = 100, Cena = 40000, AgentId = 1 });
            nekretnine.Add(new Nekretnina { Id = 2, Mesto = "Beograd", Oznaka = "Nek02", GodinaIzgradnje = 1985, Kvadratura = 100, Cena = 40000, AgentId = 2 });

            var mockRepository = new Mock<INekretnina>();
            mockRepository.Setup(x => x.GetAll()).Returns(nekretnine.AsEnumerable());////OVDE Ubacis npr. GETPretraga
            var controller = new NekretnineController(mockRepository.Object);

            // Act
            IEnumerable<Nekretnina> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nekretnine.Count, result.ToList().Count);
            Assert.AreEqual(nekretnine.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(nekretnine.ElementAt(1), result.ElementAt(1));
        }

    }
}
