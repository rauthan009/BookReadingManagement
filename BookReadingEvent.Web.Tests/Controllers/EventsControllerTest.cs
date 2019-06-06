using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookReadingEvent.Web.Controllers;

namespace BookReadingEvent.Web.Tests.Controllers
{
    [TestClass]
    public class EventsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            EventsController controller = new EventsController();


            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


    }
}
