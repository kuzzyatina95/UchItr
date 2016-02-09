using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UchItr.Startup))]
namespace UchItr
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
