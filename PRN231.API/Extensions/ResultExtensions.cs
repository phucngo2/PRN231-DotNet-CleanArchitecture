using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using PRN231.Domain.Exceptions.Common;
using PRN231.Domain.Models;

namespace PRN231.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToResult<TResult>(this Result<TResult> result)
    {
        return result.Match(
            obj => new OkObjectResult(obj),
            CreateErrorResult
        );
    }

    public static IActionResult ToResult<TResult, TContract>(this Result<TResult> result, Func<TResult, TContract> mapper)
    {
        return result.Match(
            obj => new OkObjectResult(mapper(obj)),
            CreateErrorResult
        );
    }

    private static ObjectResult CreateErrorResult(Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var response = new ExceptionResponse
        {
            Message = exception.Message,
            StatusCode = statusCode
        };

        return new ObjectResult(response)
        {
            StatusCode = statusCode
        };
    }

    private static int GetStatusCode(Exception exception) => exception switch
    {
        BadRequestException => StatusCodes.Status400BadRequest,
        UnauthorizedException => StatusCodes.Status401Unauthorized,
        NotFoundException => StatusCodes.Status404NotFound,
        ConflictException => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status500InternalServerError
    };
}
