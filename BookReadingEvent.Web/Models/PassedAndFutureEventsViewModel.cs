using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReadingEvent.Web.Models
{
    /// <summary>
    /// This class contains two enumerables of type eventview model
    /// </summary>
    public class PassedAndFutureEventsViewModel
    {
        public IEnumerable<EventViewModel> FutureEvents { get; set; }
        public IEnumerable<EventViewModel> PassedEvents { get; set; }
    }
}