using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyAspNetCoreApp.Web.Filters
{
    public class LogFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine("action method before work");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("method of action after work");
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Debug.WriteLine("action method before the result is produced");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Debug.WriteLine("action method after the result is produced");
        }
    }
}
