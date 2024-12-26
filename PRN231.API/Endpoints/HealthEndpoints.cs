using Carter;
using Microsoft.AspNetCore.Http.HttpResults;

namespace PRN231.API.Endpoints;

public class HealthEndpoints : ICarterModule
{
    private const string _apiEndpoint = "api/Health";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        //var group = app.MapGroup(_apiEndpoint).RequireAuthorization("");
        //group.MapGet("{id}", () =>
        //{
        //    return Results.Ok("Hello, 世界!");
        //});

        var group = app.MapGroup(_apiEndpoint);

        group.MapGet("", HealthCheck);
    }

    public static Results<Ok<string>, BadRequest<string>> HealthCheck()
    {
        return TypedResults.Ok("Hello, 世界!");
    }
}
