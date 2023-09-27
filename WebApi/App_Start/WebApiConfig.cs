using System;
using System.Linq;
using System.Web.Http;
using WebApi.Filters;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Web API configuration and services
            GlobalConfiguration.Configuration.Filters.Add(new GloablExceptionFilter());

            //Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
