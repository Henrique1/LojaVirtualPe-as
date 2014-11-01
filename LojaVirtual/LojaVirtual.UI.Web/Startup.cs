using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LojaVirtual.UI.Web.Startup))]
namespace LojaVirtual.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
