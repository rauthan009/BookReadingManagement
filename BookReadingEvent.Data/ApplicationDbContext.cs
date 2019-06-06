using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookReadingEvent.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public IDbSet<Event> Events { get; set; }

        public IDbSet<Invites> Invites { get; set; }
        public IDbSet<Comment> Comments { get; set; }

        public ApplicationDbContext()
            : base("EventConnection", throwIfV1Schema: false)
        {
            
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}