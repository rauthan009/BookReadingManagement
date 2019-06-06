using System.Web.Mvc;

namespace BookReadingEvent.Web.Controllers
{
    
    public class customersupportController : Controller
    {
        // GET: customersupport
        public ActionResult Index()
        {
            return Redirect("https://helpdesk.nagarro.com");
        }
    }
}