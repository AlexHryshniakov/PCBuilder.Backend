using MediatR;

namespace PCBuilder.Application.Services.CpuService.Commands.DeleteCpu;

public class DeleteCpuCommand:IRequest
{
    public Guid Id { get; set; }
}