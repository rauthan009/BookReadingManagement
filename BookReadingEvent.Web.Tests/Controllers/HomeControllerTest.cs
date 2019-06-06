using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookReadingEvent.Web.Controllers;

namespace BookReadingEvent.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();


            // Act
            ViewResult result = controller.DetailsByID(5) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


    }
}
