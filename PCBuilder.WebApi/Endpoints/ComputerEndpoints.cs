
using Microsoft.AspNetCore.Mvc;
using PCBuidler.Domain.Enums;
using PCBuilder.WebApi.Extensions;

namespace PCBuilder.WebApi.Endpoints;

public static class ComputerEndpoints
{
    public static IEndpointRouteBuilder MapComputerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("Computers", GetComputers).RequirePermissions(Permission.ReadPc);
        app.MapGet("Computers/Add", AddComputer).RequirePermissions(Permission.CreatePc);

        return app;
    }

    public static Task<IResult> GetComputers()
    {
        return Task.FromResult(Results.Ok("list of Computers"));
    }
    
    public static Task<IResult> AddComputer()
    {
        return Task.FromResult(Results.Ok("Add Computer"));
    }
}