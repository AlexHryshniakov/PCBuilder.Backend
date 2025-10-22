using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.API.Contracts.Cpus;
using PCBuilder.Application.Services.CpuService.Commands.CreateCpu;
using PCBuilder.Application.Services.CpuService.Commands.DeleteCpu;
using PCBuilder.Application.Services.CpuService.Commands.UpdateCpuPhoto;
using PCBuilder.Application.Services.CpuService.Queries.GetCpuDetails;

namespace PCBuilder.API.Endpoints;

public static class CpuEndpoints
{
    public static IEndpointRouteBuilder MapCpuEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("cpu/create", Create).DisableAntiforgery();
        app.MapDelete("cpu/delete", Delete);
        app.MapPut("cpu/update/photo", UpdatePhoto).DisableAntiforgery();
        app.MapGet("cpu/{id}", GetDetails).DisableAntiforgery();
        return app;
    }

    public static async Task<IResult> Create(
        [FromForm] CreateCpuRequest request,
        [FromServices]IMediator mediator,
        [FromServices]IMapper mapper,
        CancellationToken ct)
    {
        var command = mapper.Map<CreateCpuCommand>(request);
        var id = await mediator.Send(command, ct);
        return Results.Ok(id);
    } 
    
    public static async Task<IResult> Delete(
        Guid cpuId,
        [FromServices]IMediator mediator,
        [FromServices]IMapper mapper,
        CancellationToken ct)
    {
        var command = new DeleteCpuCommand{Id=cpuId};
        await mediator.Send(command, ct);
        return Results.Ok();
    } 
    
    public static async Task<IResult> UpdatePhoto(
        [FromForm] UpdateCpuPhotoRequest photoRequest,
        [FromServices]IMediator mediator,
        [FromServices]IMapper mapper,
        CancellationToken ct)
    {
        var command = mapper.Map<UpdateCpuPhotoCommand>(photoRequest);
        await mediator.Send(command, ct);
        return Results.Ok();
    } 
    
    public static async Task<IResult> GetDetails(
        [FromRoute] Guid id,
        [FromServices]IMediator mediator,
        CancellationToken ct)
    {
        var command = new GetCpuDetailsQuery{Id=id};
        var vm = await mediator.Send(command, ct);
        return Results.Ok(vm);
    } 
}