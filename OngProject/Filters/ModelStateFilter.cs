using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OngProject.Filters
{
    public class ModelStateFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(modelState);
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}