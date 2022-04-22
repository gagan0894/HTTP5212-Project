using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HTTP5212_Project.Startup))]
namespace HTTP5212_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
