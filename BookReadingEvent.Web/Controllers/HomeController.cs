using BookReadingEvent.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BookReadingEvent.Business;

namespace BookReadingEvent.Web.Controllers
{
    public class HomeController : BaseController
    {

        /// <summary>
        /// The Default Action of the home controller, it returns a view which displays all public events
        /// </summary>
        /// <returns>Returns the Index View</returns>
        public ActionResult Index()
        {
            string currentUserId = this.User.Identity.GetUserId();
            var isAdmin = this.IsAdmin();
            var events = repository.GetEvents()
                .Where(e => e.IsPublic || isAdmin || (e.AuthorId != null && e.AuthorId == currentUserId))
                .Select(EventViewModel.ViewModel);

            var futureEvents = events.Where(e => e.DateAndTime > DateTime.Now);
            var passedEvents = events.Where(e => e.DateAndTime <= DateTime.Now);
            return View(new PassedAndFutureEventsViewModel()
            {
                FutureEvents = futureEvents,
                PassedEvents = passedEvents
            });
        }

        /// <summary>
        /// This Action gets details of a specific event by eventID
        /// </summary>
        /// <param name="ID">ID paramater is the eventID for a event</param>
        /// <returns>returns the details page view</returns>
        public ActionResult DetailsByID(int? ID)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = this.IsAdmin();
            var eventDetails = repository.GetEvents()
                .Where(e => e.EventID == ID)
                .Where(e => e.IsPublic || isAdmin || (e.AuthorId != null && e.AuthorId == currentUserId))
                .Select(EventDetailsViewModel.ViewModel)
                .FirstOrDefault();

            var isOwner = (eventDetails != null && eventDetails.AuthorId != null &&
                eventDetails.AuthorId == currentUserId);
            this.ViewBag.CanEdit = isOwner || isAdmin;

            eventDetails.Date = eventDetails.DateTime.ToShortDateString();
            eventDetails.StartTime = eventDetails.DateTime.ToShortTimeString();
            return View(eventDetails);
     
        }

    }
}