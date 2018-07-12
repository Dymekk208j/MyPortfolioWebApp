using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyPortfolioWebApp.Startup))]
namespace MyPortfolioWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
