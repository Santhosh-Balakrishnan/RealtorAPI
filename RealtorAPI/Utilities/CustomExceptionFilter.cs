using Microsoft.AspNetCore.Mvc.Filters;

namespace RealtorAPI.Utilities
{
    public class CustomExceptionFilter: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if(context.Exception is ArithmeticException)
            {

            }
            throw(context.Exception);
        }
    }
}
