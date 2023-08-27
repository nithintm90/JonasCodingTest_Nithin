using System.Web;
using System.Web.Http;
using Serilog;
using WebApi.App_Start;

namespace WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            SerilogConfig.Setup();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_End()
        {
            Log.CloseAndFlush();
        }
    }
}
