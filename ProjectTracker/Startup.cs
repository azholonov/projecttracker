using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectTracker.Startup))]
namespace ProjectTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
