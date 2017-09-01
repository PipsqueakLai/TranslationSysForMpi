using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TranslationSys.Startup))]
namespace TranslationSys
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
