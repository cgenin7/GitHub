using Microsoft.Owin;
using Owin;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(APMPBilling.Startup))]
namespace APMPBilling
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ModelBinders.Binders.Add(typeof(float), new FloatModelBinder());
        }
    }
}
