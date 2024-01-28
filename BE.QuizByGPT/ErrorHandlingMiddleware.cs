using System.Diagnostics;
using System.Net;
using System.Text.Json;
using BE.QuizByGPT.BLL.Models;

namespace BE.QuizByGPT
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorHandlingModel = new ErrorHandlingModel();
            var exceptionType = exception.GetType();

            var status = exceptionType.Name switch
            {
                nameof(BadHttpRequestException) => HttpStatusCode.BadRequest,
                nameof(KeyNotFoundException) => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };

            errorHandlingModel.Message = exception.Message;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                errorHandlingModel.StackTrace = exception.StackTrace;
            }

            var result = JsonSerializer.Serialize(errorHandlingModel);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            await context.Response.WriteAsync(result);
        }
    }

}
