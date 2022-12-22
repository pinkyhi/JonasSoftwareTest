using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace WebApi.Filters
{
    public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if(context.Exception is KeyNotFoundException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
                context.Response.Content = new StringContent(context.Exception.Message);
            }
            else if (context.Exception is ArgumentException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                context.Response.Content = new StringContent(context.Exception.Message);
            }
            else if (context.Exception is NotImplementedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            else
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                context.Response.Content = new StringContent(context.Exception.Message);
            }
        }
    }
}