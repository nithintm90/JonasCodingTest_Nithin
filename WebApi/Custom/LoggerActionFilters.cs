using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi.Custom
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class LoggerActionFilter : ActionFilterAttribute
    {
        //before
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var actionName = actionContext.ActionDescriptor.ActionName;
            var controllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            Logger.LogInfo($"Starting execution-----> Action: '{actionName}' --- Controller: '{controllerName}'"); //Can add further request details too if need
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
        //after 
        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            var controllerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            Logger.LogInfo($"Finished execution-----> Action: '{actionName}' --- Controller: '{controllerName}'"); //Can add further request details too if need
            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }
}