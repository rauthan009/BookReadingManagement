using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookReadingEvent.Web.Startup))]
namespace BookReadingEvent.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
