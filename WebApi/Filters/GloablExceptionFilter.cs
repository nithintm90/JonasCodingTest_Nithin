using BusinessLayer.Exceptions;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace WebApi.Filters
{
    public class GloablExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is EntityNotFoundException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            if (context.Exception is EntityConflictException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Conflict);
            }
        }
    }
}