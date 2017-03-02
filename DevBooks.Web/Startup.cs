using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevBooks.Web.Startup))]
namespace DevBooks.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
