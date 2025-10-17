using AutoMapper;
using PCBuilder.Core.Models;
using PCBuilder.Application.Common.Mapping;

namespace PCBuilder.Application.Services.UserService.Queries.GetUserDetails;

public class GetUserDetailsQueryVm:IMapWith<User>
{
    public Guid Id { get; set; }
    public string UserName{ get; set;} = String.Empty;
    public string Email{ get;set; } = String.Empty;
    public string AvatarUrl{get; set;} = String.Empty;

    public void Mapping(Profile profile) =>
        profile.CreateMap<User, GetUserDetailsQueryVm>();
}