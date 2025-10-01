using MediatR;
using PCBuidler.Domain.Models;

namespace PCBuilder.Application.Services.UserService.Queries.GetUserDetails;

public class GetUserDetailsQuery:IRequest<GetUserDetailsQueryVm>
{
    public Guid UserId { get; set; }    
}