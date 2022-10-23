using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Newtonsoft.Json;

namespace WebApi.Filters
{
    public class ActivityLoggingFilter : ActionFilterAttribute
    {

        private readonly ILoggerRepository _iLogSvc;

        public ActivityLoggingFilter(ILoggerRepository iLogSvc)
        {
            _iLogSvc = iLogSvc;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // request has successfully executed.
            if (actionExecutedContext.Exception == null)
            {
                // retrieve request information
                var postedData = (new Dictionary<string, object>.ValueCollection(actionExecutedContext.ActionContext.ActionArguments));

                // retrieve response information
                var objectContent = actionExecutedContext.Response.Content as ObjectContent;
                int statusCode = (int)actionExecutedContext.Response.StatusCode;
                string responseContent = null;
                if (objectContent != null)
                {
                    responseContent = JsonConvert.SerializeObject(objectContent.Value); // get response content
                }

                _iLogSvc.SaveLogAsync(new LogEntity()
                {
                    Guid = Guid.NewGuid(),
                    RequestUri = actionExecutedContext.Request.RequestUri.ToString(),
                    RequestMethod = actionExecutedContext.Request.Method.ToString(),
                    RequestContent = JsonConvert.SerializeObject(postedData),
                    StatusCode = (int)actionExecutedContext.Response.StatusCode,
                    ResponseContent = responseContent,
                    Source = "",
                    Message = "",
                    StackTrace = "",
                    IssuedAt = DateTime.Now
                });
            }
        }

    }
}