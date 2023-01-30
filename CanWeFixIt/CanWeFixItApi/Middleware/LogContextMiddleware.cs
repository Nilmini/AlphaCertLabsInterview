using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace CanWeFixItApi.Middleware
{
    public class LogContextMiddleware : IMiddleware
    {
        private readonly string correlationIdHeader = "X-Corralation-Id";

        public LogContextMiddleware()
        {

        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            using (LogContext.PushProperty(correlationIdHeader, Guid.NewGuid()))
            {
                return next(context);
            }
        }
    }
}
