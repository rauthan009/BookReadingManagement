using System.ComponentModel.DataAnnotations;

namespace BookReadingEvent.Data
{
    public class Invites
    {
        [Key]
        public int InviteId { get; set; }
        public int EventsId { get; set; }

        public string EmailId { get; set; }
    }
}
