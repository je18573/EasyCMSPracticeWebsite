using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMS_Practice.Startup))]
namespace CMS_Practice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
