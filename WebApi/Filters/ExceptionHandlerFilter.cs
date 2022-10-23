using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Newtonsoft.Json;

namespace WebApi.Filters
{
    public class ExceptionHandlerFilter : ExceptionFilterAttribute
    {
        private readonly ILoggerRepository _iLogSvc;

        public ExceptionHandlerFilter(ILoggerRepository iLogSvc)
        {
            _iLogSvc = iLogSvc;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {

                // get posted data from the current context
                var postedData = (new Dictionary<string, object>.ValueCollection(actionExecutedContext.ActionContext.ActionArguments));
                string responseContent = null;

                if (actionExecutedContext.Exception is BusinessException)
                {
                    var businessException = actionExecutedContext.Exception as BusinessException;
                    var errorMessagError = new System.Web.Http.HttpError(businessException.ErrorDescription);
                    responseContent = businessException.ErrorDescription;
                    if (businessException.ErrorCode == 501)
                    {
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.NotImplemented, errorMessagError);
                    }
                }
                else
                {
                    var errorMessagError = new System.Web.Http.HttpError("Transaction failed. Please contact your administrator");
                    actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMessagError);
                }

                // log error entry
                _iLogSvc.SaveLogAsync(new LogEntity()
                {
                    Guid = Guid.NewGuid(),
                    RequestUri = actionExecutedContext.Request.RequestUri.ToString(),
                    RequestMethod = actionExecutedContext.Request.Method.ToString(),
                    RequestContent = JsonConvert.SerializeObject(postedData),
                    StatusCode = (int)actionExecutedContext.Response.StatusCode,
                    ResponseContent = responseContent,
                    Source = actionExecutedContext.Exception.Source,
                    Message = actionExecutedContext.Exception.Message,
                    StackTrace = actionExecutedContext.Exception.StackTrace,
                    IssuedAt = DateTime.Now
                });
            }
            catch (Exception ex)
            {

            }
        }

    }

}