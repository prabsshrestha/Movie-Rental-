using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Prabesh.Startup))]
namespace Prabesh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
