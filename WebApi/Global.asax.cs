using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using AutoMapper;
using BusinessLayer;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Database;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using DataAccessLayer.Repositories;
using Microsoft.Owin.Logging;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using WebApi.Filters;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            var container = new Container();
            var profiles = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => typeof(AutoMapper.Profile).IsAssignableFrom(x));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BusinessProfile>();
                cfg.AddProfile<AppServicesProfile>();
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(Activator.CreateInstance(profile) as AutoMapper.Profile);
                }
            });
            container.RegisterInstance(config);
            container.RegisterSingleton(() => config.CreateMapper(container.GetInstance));
            container.RegisterSingleton<ILogWrapper<LogEntity>, LoggerDatabase<LogEntity>>();
            container.RegisterSingleton<ILoggerRepository, LoggerRepository>();
            container.RegisterSingleton<IDbWrapper<Company>, InMemoryDatabase<Company>>();
            container.RegisterSingleton<ICompanyService, CompanyService>();
            container.RegisterSingleton<ICompanyRepository, CompanyRepository>();
            container.RegisterSingleton<IDbWrapper<Employee>, InMemoryDatabase<Employee>>();
            container.RegisterSingleton<IEmployeeService, EmployeeService>();
            container.RegisterSingleton<IEmployeeRepository, EmployeeRepository>();
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);


            GlobalConfiguration.Configuration.Filters.Add(new ExceptionHandlerFilter(container.GetInstance<ILoggerRepository>()));
            GlobalConfiguration.Configuration.Filters.Add(new ActivityLoggingFilter(container.GetInstance<ILoggerRepository>()));

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
