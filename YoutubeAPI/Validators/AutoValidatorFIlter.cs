using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace YoutubeAPI.Validators
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                var response = new
                {
                    Message = "Validation failed",
                    Errors = errors
                };

                context.Result = new BadRequestObjectResult(response);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) {}
    }
}