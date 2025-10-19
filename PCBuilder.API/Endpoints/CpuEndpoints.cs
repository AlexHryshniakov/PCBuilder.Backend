using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.API.Contracts.Cpus;
using PCBuilder.Application.Services.CpuService.Commands.CreateCpu;

namespace PCBuilder.API.Endpoints;

public static class CpuEndpoints
{
    public static IEndpointRouteBuilder MapCpuEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("cpu/create", Create).RequireAuthorization().DisableAntiforgery();
        return app;
    }

    public static async Task<IResult> Create(
        [FromForm] CreateCpuRequest request,
        [FromServices]IMediator mediator,
        [FromServices]IMapper mapper,
        CancellationToken ct)
    {
        var command = mapper.Map<CreateCpuCommand>(request);
        await mediator.Send(command, ct);
        return Results.Ok();
        var id = await mediator.Send(command, ct);
        return Results.Ok(id);
    } 
}