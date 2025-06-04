using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using PRN231.Domain.Common;
using PRN231.Domain.Models;

namespace PRN231.Domain.Extensions;

public static class ResultExtentions
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

    public static ActionResult<TResult> ToActionResult<TResult>(this Result<TResult> result)
    {
        return result.Match(
            obj => new OkObjectResult(obj),
            CreateErrorResult
        );
    }

    public static ActionResult<TResult> ToActionResult<TResult, TContract>(this Result<TResult> result, Func<TResult, TContract> mapper)
    {
        return result.Match(
            obj => new OkObjectResult(mapper(obj)),
            CreateErrorResult
        );
    }

    public static TResult? ToValue<TResult>(this Result<TResult> result) where TResult : class
    {
        return result.Match(
            Succ: value => value,
            Fail: _ => default
        );
    }

    public static TResult ToValueStruct<TResult>(this Result<TResult> result) where TResult : struct
    {
        return result.Match(
            Succ: value => value,
            Fail: _ => default
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
