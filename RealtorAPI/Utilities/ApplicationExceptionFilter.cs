using System.Net;

namespace RealtorAPI.Utilities
{
    public class ApplicationExceptionMiddleWare
    {
        private readonly RequestDelegate request;
        public ApplicationExceptionMiddleWare(RequestDelegate next)
        {
            request = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await request(httpContext);
            }
            catch(Exception exception)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                var content = new { Error = "Unhandled Exception", Message = exception.StackTrace };
                await response.WriteAsJsonAsync(content);
            }
        }
    }
}
