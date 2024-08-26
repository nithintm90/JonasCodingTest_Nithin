using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            NLog.LogManager.Setup().LoadConfigurationFromXml("NLog.config");

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
