using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Register.Api.Controllers;

public class ApiResponseFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult && context.Exception == null)
        {
            if (objectResult.StatusCode == StatusCodes.Status204NoContent)
            {
                context.Result = new ObjectResult(new
                {
                    success = true
                })
                {
                    StatusCode = StatusCodes.Status204NoContent
                };
                return;
            }

            context.Result = new ObjectResult(new
            {
                success = true,
                data = objectResult.Value
            })
            {
                StatusCode = objectResult.StatusCode
            };
        }
    }
}