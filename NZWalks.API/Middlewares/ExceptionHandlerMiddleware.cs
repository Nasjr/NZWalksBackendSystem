using Microsoft.AspNetCore.Http;
using System.Net;

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // log exception
               
                // Custom Error Response
                var errorId = Guid.NewGuid();
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                
                var error = new
                {
                    Id = errorId.ToString(),
                    ErrorMessage = "Something went wring! we are looking into resolving this"
                };
                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
