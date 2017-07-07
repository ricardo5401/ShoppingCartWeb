using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ShoppingCartWeb.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        public LogActionFilter(ILoggerFactory loggerFactory){
             _logger = loggerFactory.CreateLogger("LogActionFilter");
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string action  = context.ActionDescriptor.DisplayName;
            _logger.LogWarning("LogActionFilter OnActionExecuting " +  action);
            base.OnActionExecuting(context);
        }
 
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string action  = context.ActionDescriptor.DisplayName;
            _logger.LogWarning("LogActionFilter OnActionExecuted " + " " + action);
            base.OnActionExecuted(context);
        }
 
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            string action  = context.ActionDescriptor.DisplayName;
            _logger.LogWarning("LogActionFilter OnResultExecuting " + action);
            base.OnResultExecuting(context);
        }
 
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            string action  = context.ActionDescriptor.DisplayName;
            _logger.LogWarning("LogActionFilter OnResultExecuted " + action);
            base.OnResultExecuted(context);
        }
    }
}