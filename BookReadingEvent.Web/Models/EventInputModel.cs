using BookReadingEvent.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookReadingEvent.Web.Models
{
    /// <summary>
    /// This is the eventinput model which contains all the validation requirenments for the Input
    /// </summary>
    public class EventInputModel
    {
        [Required(ErrorMessage = "Event title is required.")]
        [StringLength(200, ErrorMessage = "The {0} must be between {2} and {1} characters long.",
                    MinimumLength = 1)]
        [Display(Name = "Title *")]
        [Key]
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date and Time ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Event Location is required.")]
        [MaxLength(200)]
        public string Location { get; set; }

        [Display(Name = "Is Public?")]
        public bool IsPublic { get; set; }

        public int Duration { get; set; }
        public string Description { get; set; }
        public string OtherDetails { get; set; }
        public string InviteList { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public EventInputModel()
        {
            Event @event = new Event();
            Date = @event.DateAndTime;
            IsPublic = @event.IsPublic;
        }

        /// <summary>
        /// Create an input model from the event
        /// </summary>
        /// <param name="events">events is the passed event object</param>
        /// <returns>Returns a new Event Input Model</returns>
        
        public static EventInputModel CreateFromEvent(Event events)
        {
            return new EventInputModel()
            {
                Title = events.Title,
                Date = events.DateAndTime,
                Duration = events.Duration,
                Location = events.Location,
                Description = events.Description,
                OtherDetails = events.OtherDetails,
                InviteList = events.InviteList,
                IsPublic = events.IsPublic
            };
        }
    }
}