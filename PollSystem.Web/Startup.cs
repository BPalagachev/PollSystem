using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PollSystem.Web.Startup))]
namespace PollSystem.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
