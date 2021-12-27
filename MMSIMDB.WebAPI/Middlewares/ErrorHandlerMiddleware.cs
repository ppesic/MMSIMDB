using MMSIMDB.Application.Exceptions;
using MMSIMDB.Application.Wrappers;
using System.Text.Json;

namespace MMSIMDB.WebAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>(String.Join("<br/>", error?.Errors)); 
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>(error?.Message);

                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
