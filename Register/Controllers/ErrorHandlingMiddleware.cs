using FluentValidation;
using System.Net;
using System.Reflection;

namespace Register.Api.Controllers;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await WriteErrorResponse(context, HttpStatusCode.BadRequest, ex.Errors.Select(e => e.ErrorMessage));
        }
        catch (UnauthorizedAccessException ex)
        {
            await WriteErrorResponse(context, HttpStatusCode.Unauthorized, new[] { ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            await WriteErrorResponse(context, HttpStatusCode.NotFound, new[] { ex.Message });
        }
        catch (Exception ex)
        {
            var realException = ex is TargetInvocationException && ex.InnerException != null
                ? ex.InnerException
                : ex;

            await WriteErrorResponse(context, HttpStatusCode.InternalServerError, new[] { realException.Message });
        }
    }

    private static async Task WriteErrorResponse(HttpContext context, HttpStatusCode statusCode, IEnumerable<string> errors)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            success = false,
            errors = errors.ToArray()
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}
