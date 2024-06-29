namespace FightForge.Exceptions
{
    public class AppExceptionsHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (int statusCode, string errorMessage) = exception switch
            {
                BadRequestException badRequestException => (400, badRequestException.Message),

                _ => (500, "Something went wrong")
            };
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(errorMessage);
            return true;
        }
    }
}
