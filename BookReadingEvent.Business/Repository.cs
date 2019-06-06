
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReadingEvent.Data;

namespace BookReadingEvent.Business
{
    public interface IRepository
    {
        IQueryable<Event> GetEvents();
        IQueryable<ApplicationUser> GetUsers();
        IQueryable<Invites> GetInvites();

    }
     public class Repository:IRepository
     {
        protected ApplicationDbContext ApplicationDb = new ApplicationDbContext();

        public IQueryable<Event> GetEvents()
        {
            return ApplicationDb.Events
                .OrderBy(e => e.DateAndTime);
        }

        public IQueryable<Invites> GetInvites()
        {
            return ApplicationDb.Invites;
        }

        public IQueryable<ApplicationUser> GetUsers()
        {
            return ApplicationDb.Users;
        }
    }
}
