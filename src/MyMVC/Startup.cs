using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyMvc.Startup))]
namespace MyMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
