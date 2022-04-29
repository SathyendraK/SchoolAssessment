using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchoolAssessment.Startup))]
namespace SchoolAssessment
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
