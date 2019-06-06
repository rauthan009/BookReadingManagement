using System;
using BookReadingEvent.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using System.Linq;
using BookReadingEvent.Web.Controllers;
using System.Web.Mvc;

namespace BookReadingEvent.Web.Tests.Controllers
{
    [TestClass]
    public class BaseControllerTest
    {
        ApplicationDbContext Db = new ApplicationDbContext();
        [TestMethod]
        public void IsAdmin()
        {
            //Arrange
            var currentUserId = "87581c84 - 3714 - 4984 - 84be - 96f3a59da5c5";

            //Act
            var adminID = Db.Users.Where(e => e.Id == currentUserId).ToString();

            //Assert
            Assert.AreNotEqual(adminID, currentUserId);
        }


    }
}
