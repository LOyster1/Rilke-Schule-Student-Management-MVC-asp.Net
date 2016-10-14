using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rilke_Schule_Student_Management.Startup))]
namespace Rilke_Schule_Student_Management
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
