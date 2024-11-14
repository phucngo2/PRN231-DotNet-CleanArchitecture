using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using PRN231.Domain.Common;
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
        var statusCode = StatusCodeHelpers.ExceptionToStatusCode(exception);
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
}
