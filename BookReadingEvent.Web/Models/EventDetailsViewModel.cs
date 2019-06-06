using BookReadingEvent.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BookReadingEvent.Web.Models
{
    /// <summary>
    /// View Model the details of an individual event
    /// </summary>
    public class EventDetailsViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public string StartTime { get; set; }

        public int Duration { get; set; }
        public string Description { get; set; }
        public string OtherDetails { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
        public string AuthorId { get; set; }
    
        public int Invited { get; set; }
        public bool IsPublic { get; set; }

        /// <summary>
        /// An Expression that takes Event as input and returns a new eventviewmodel by first copying all the required information into it from the event
        /// </summary>
        public static Expression<Func<Event, EventDetailsViewModel>> ViewModel
        {
            get
            {
                return e => new EventDetailsViewModel()
                {
                    ID = e.EventID,
                    Title = e.Title,
                    DateTime = e.DateAndTime,
                    Location = e.Location,
                    Duration = e.Duration,
                    Description = e.Description,
                    OtherDetails = e.OtherDetails,
                    AuthorId = e.AuthorId,
                    Invited = e.TotalInvited,
                    Comments = e.Comments.AsQueryable().Select(CommentViewModel.ViewModel),
                };
            }
        }
    }
}