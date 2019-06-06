using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookReadingEvent.Data;
using BookReadingEvent.Web.Models;
using Microsoft.AspNet.Identity;

namespace BookReadingEvent.Web.Controllers
{
    /// <summary>
    /// The Events controller holds all the Actions related to any opertion on a event
    /// </summary>
    [Authorize]
    public class EventsController : BaseController
    {
        /// <summary>
        /// This Action gets all the events created by the logged in user
        /// </summary>
        /// <returns>Returns a view with all the events created by the user</returns>
        public ActionResult MyEvents()
        {
            string currentUserID = User.Identity.GetUserId();
            var events = repository.GetEvents()
                .Where(e => e.AuthorId == currentUserID)
                .OrderBy(e => e.DateAndTime)
                .Select(e => e);
            return View(events);

        }

        
        [HttpGet]
        public ActionResult Create()
        {
            //The following code sets the default value of date to today's date which can be changed later by the user
            EventInputModel model = new EventInputModel();
            //model.Date = DateTime.Now;

            return View(model);
        }

        /// <summary>
        /// Create action for the event, it gets called after the user posts data from the form
        /// </summary>
        /// <param name="model">Accepts the model as a parameter</param>
        /// <returns>Redirects to my events action on success and back to create view on failure </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventInputModel model)
        {
            if(model!=null&&this.ModelState.IsValid)
            {
                //creating a temp event type of object which will be passed to database to be added
                var tempEvent = new Event()
                {
                    AuthorId = this.User.Identity.GetUserId(),
                    Title = model.Title,
                    DateAndTime = model.Date,
                    Duration = model.Duration,
                    Description = model.Description,
                    OtherDetails = model.OtherDetails,
                    Location = model.Location,
                    IsPublic = model.IsPublic,
                    InviteList = model.InviteList
                    
                };

                if(tempEvent.InviteList==null)
                {
                    this.dataBase.Events.Add(tempEvent);
                    this.dataBase.SaveChanges();
                    TempData["SuccessMessage"] = "Event Created Succesfuly";
                    return this.RedirectToAction("MyEvents");
                }
                else
                {
                    AddIntoInvites(tempEvent,true);
                    TempData["SuccessMessage"] = "Event Created Succesfuly";
                    return this.RedirectToAction("MyEvents");
                }
            }
            return this.View(model);
        }

        /// <summary>
        /// The method populates the Invite table with by splitting the invitelist separated by a comma
        /// </summary>
        /// <param name="tempEvent">The tempmodel is passed here </param>
        private void AddIntoInvites(Event tempEvent,bool Add)
        {
            List<string> names = tempEvent.InviteList.Split(',').ToList<string>();
            names.Reverse();

            tempEvent.TotalInvited = names.Count();
            if(Add)
            { 
                dataBase.Events.Add(tempEvent);
                dataBase.SaveChanges();
            }

            foreach (string inviteMail in names)
            {
                Invites tempInvite = new Invites()
                {
                    EventsId = tempEvent.EventID,
                    EmailId = inviteMail
                };

                this.dataBase.Database.Log = Console.Write;
                this.dataBase.Invites.Add(tempInvite);
                this.dataBase.SaveChanges();
            }
        }
        /// <summary>
        /// Get:Edit for displaying previously stored data to the user
        /// </summary>
        /// <param name="id">Id of the event</param>
        /// <returns>Returns edit view and populates it with the previous data</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Event eventToEdit = LoadEvent(id);
            if(eventToEdit==null||eventToEdit.DateAndTime<DateTime.Today)
            {
                TempData["Fail"] = "Passed event cannot be edited";
                return RedirectToAction("MyEvents");
            }

            var model = EventInputModel.CreateFromEvent(eventToEdit);
            return View(model);
        }

        /// <summary>
        /// Loads the data from the database and passes it back to the edit action
        /// </summary>
        /// <param name="id">Id of the event</param>
        /// <returns>Return Event</returns>
        private Event LoadEvent(int id)
        {
            string currentUserId = this.User.Identity.GetUserId();
            bool isAdmin = this.IsAdmin();
            Event eventToEdit = this.dataBase.Events
                .Where(e => e.EventID == id)
                .FirstOrDefault(e => e.AuthorId == currentUserId || isAdmin);
            return eventToEdit;
        }

        //Post:Edit action that gets the data from the form and then updates the data into the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EventInputModel model)
        {
            Event eventToEdit = LoadEvent(id);
            if (eventToEdit == null)
            {
                return RedirectToAction("MyEvents");
            }

            if (model != null && ModelState.IsValid)
            {
                eventToEdit.Title = model.Title;
                eventToEdit.DateAndTime = model.Date;
                eventToEdit.Duration = model.Duration;
                eventToEdit.Description = model.Description;
                eventToEdit.OtherDetails = model.OtherDetails;
                eventToEdit.Location = model.Location;
                eventToEdit.IsPublic = model.IsPublic;
                eventToEdit.InviteList = model.InviteList;
                dataBase.Database.Log = Console.Write;
                dataBase.SaveChanges();
                UpdateInvites(eventToEdit);
                TempData["SuccessMessage"] = "Event Succesfully Edited";
                return this.RedirectToAction("MyEvents");
            }

            return this.View(model);
        }

        private void UpdateInvites(Event eventToEdit)
        {
            List<Invites> invitesList = dataBase.Invites
                            .Where(i => i.EventsId == eventToEdit.EventID)
                            .Select(i => i).ToList();
            foreach (Invites invite in invitesList)
            {
                dataBase.Invites.Remove(invite);
            }
            if (eventToEdit.InviteList != null )
            {
                AddIntoInvites(eventToEdit,false);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Event eventToDelete = LoadEvent(id);
            if (eventToDelete == null)
            {
                return this.RedirectToAction("MyEvents");
            }

            var model = EventInputModel.CreateFromEvent(eventToDelete);
            return View(model);
        }

        //Post:deletes an event entry depending on the id provided
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EventInputModel model)
        {
            Event eventToDelete = LoadEvent(id);
            if (eventToDelete == null)
            {
                return this.RedirectToAction("MyEvents");
            }
            this.dataBase.Database.Log = Console.Write;
            this.dataBase.Events.Remove(eventToDelete);
            this.dataBase.SaveChanges();
            TempData["SuccessMessage"] = "Event Deleted Succesfuly";
            return this.RedirectToAction("MyEvents");
        }

        /// <summary>
        /// retreives events based on the user email id and returns a view displaying passed and future events
        /// </summary>
        /// <returns>returns a view by passing future and passsed events</returns>
        public ActionResult InvitedEvents()
        {
            string userEmailID = User.Identity.GetUserName();
            List<int> invitedEventID = repository.GetInvites()
                .Where(e => e.EmailId == userEmailID)
                .Select(e => e.EventsId).ToList(); 

            
            var events = this.dataBase.Events
                        .Where(e => invitedEventID.Contains(e.EventID))
                        .Select(EventViewModel.ViewModel);
                var futureEvents = events.Where(e => e.DateAndTime > DateTime.Now);
                var passedEvents = events.Where(e => e.DateAndTime < DateTime.Now);
            
            return View(new PassedAndFutureEventsViewModel()
            {
                FutureEvents = futureEvents,
                PassedEvents = passedEvents
            });
        }
    }
}