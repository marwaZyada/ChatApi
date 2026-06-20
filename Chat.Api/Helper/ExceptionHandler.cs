using Chat.Application.Exceptions;
using Chat.Application.Response;


namespace Chat.Api.Helper
{
    public static class ExceptionHandler
    {
        public static async Task HandleExceptionAsync(
       HttpContext context,
       Exception exception)
        {
            var statusCode = exception switch
            {
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                ForbiddenException => StatusCodes.Status403Forbidden,
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            if (exception is FluentValidation.ValidationException validationException)
            {
                var errors = validationException.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorMessage).ToList());

                await context.Response.WriteAsJsonAsync(
                    ApiResponse<object>.FailResponse(
                        "Validation failed",
                        errors));

                return;
            }

            await context.Response.WriteAsJsonAsync(
                ApiResponse<object>.FailResponse(exception.Message));
        }
    }
}
