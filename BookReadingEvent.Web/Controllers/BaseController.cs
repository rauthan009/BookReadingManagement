using System.Web.Mvc;
using BookReadingEvent.Data;
using Microsoft.AspNet.Identity;
using BookReadingEvent.Business;

namespace BookReadingEvent.Web.Controllers
{
    /// <summary>
    /// This class will be used as base class for all controllers in the MVC application
    /// </summary>
    public class BaseController : Controller
    {
        protected ApplicationDbContext dataBase = new ApplicationDbContext();
        protected Repository repository = new Repository();
        /// <summary>
        ///Method to check whether a user have administrator right or not
        /// </summary>
        public bool IsAdmin()
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = (currentUserId != null && this.User.IsInRole("Administrator"));
            return isAdmin;

        }
       
    }
}