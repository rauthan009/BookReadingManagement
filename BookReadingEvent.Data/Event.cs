using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookReadingEvent.Data
{
    /// <summary>
    /// The class contains all the properties of a event
    /// </summary>
    public class Event
    {
        public Event()
        {
            this.IsPublic = true;
            this.DateAndTime = DateTime.Now;
        }
        [Key]
        public int EventID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        public string AuthorId { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string OtherDetails { get; set; }
        [Required]
        [MaxLength(200)]
        public string Location { get; set; }
        public bool IsPublic { get; set; }
        public int TotalInvited { get; set; }
        public string InviteList { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
