using System.Web.Http;
using NasleGhalam.ServiceLayer.Configs;

namespace NasleGhalam.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            MappingConfig.RegisterMaps();
        }
    }
}
