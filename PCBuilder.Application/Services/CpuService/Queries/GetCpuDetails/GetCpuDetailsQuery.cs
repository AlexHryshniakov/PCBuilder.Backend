using MediatR;

namespace PCBuilder.Application.Services.CpuService.Queries.GetCpuDetails;

public class GetCpuDetailsQuery:IRequest<CpuDetailsVm>
{
    public Guid Id { get; set; }
}