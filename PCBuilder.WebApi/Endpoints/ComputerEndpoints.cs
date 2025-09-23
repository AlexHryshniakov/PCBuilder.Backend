
using Microsoft.AspNetCore.Mvc;

namespace PCBuilder.WebApi.Endpoints;

public static class ComputerEndpoints
{
    public static IEndpointRouteBuilder MapComputerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("Computers", GetComputers).RequireAuthorization();

        return app;
    }

    public static Task<IResult> GetComputers()
    {
        return Task.FromResult(Results.Ok("list of Computers"));
    }
}