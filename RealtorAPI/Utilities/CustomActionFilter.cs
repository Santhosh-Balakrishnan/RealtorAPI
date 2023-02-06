using Microsoft.AspNetCore.Mvc.Filters;

namespace RealtorAPI.Utilities
{
    public class CustomActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }

    public class CustomEndPointFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
        }
    }
}
