using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GraphsWeb.Startup))]
namespace GraphsWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
