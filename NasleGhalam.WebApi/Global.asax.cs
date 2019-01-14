using System.Web.Http;
using NasleGhalam.ServiceLayer.Configs;
using StructureMap.Web.Pipeline;

namespace NasleGhalam.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            SiteConfig.RegisterAutoMapper();
        }

        protected void Application_EndRequest()
        {
            new HybridLifecycle().FindCache(null).DisposeAndClear();
        }
    }
}
