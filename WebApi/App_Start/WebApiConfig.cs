using System;
using System.Linq;
using System.Web.Http;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Web API configuration and services

            //Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "StringParamApi",
                routeTemplate: "api/{controller}/{companyCode}",
                defaults: new { companyCode = RouteParameter.Optional },
                //accepts companyCode as strings of 3 to 10 character length
                constraints: new { companyCode = @"^[A-Za-z0-9]{3,10}$" }
            );
            config.Routes.MapHttpRoute(
                name: "StringParamEmpApi",
                routeTemplate: "api/{controller}/{employeeCode}",
                defaults: new { companyCode = RouteParameter.Optional },
                //accepts employeeCode as strings of 3 to 10 character length
                constraints: new { employeeCode = @"^[A-Za-z0-9]{3,10}$" }
            );
        }
    }
}
