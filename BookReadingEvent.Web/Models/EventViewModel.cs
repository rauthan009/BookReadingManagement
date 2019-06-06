using BookReadingEvent.Data;
using System;
using System.Linq.Expressions;

namespace BookReadingEvent.Web.Models
{
    /// <summary>
    /// Defines a Event view model
    /// </summary>
    public class EventViewModel
    {
        public int EventID { get; set; }
        public string Title { get; set; }

        public DateTime DateAndTime { get; set; }

        public int Duration { get; set; }
        public string Description { get; set; }
        public string OtherDetails { get; set; }
        public string Location { get; set; }
        public bool IsPublic { get; set; }

        /// <summary>
        /// The expression takes an event input and returns a eventviewmodel
        /// </summary>
        public static Expression<Func<Event, EventViewModel>> ViewModel
        {
            get
            {
                return e => new EventViewModel()
                {
                    EventID = e.EventID,
                    Title = e.Title,
                    DateAndTime = e.DateAndTime,
                    Duration = e.Duration,
                    Location = e.Location,
                };
            }
        }
    }
}