using Chat.Api.Helper;
using Chat.Application.Exceptions;
using Chat.Application.Response;

namespace Chat.Api.MiddleWare
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await ExceptionHandler.HandleExceptionAsync(
                    context,
                    ex);
            }
        }
    }
}
