using AutoMapper;
using PCBuilder.Application.Common.Mapping;
using PCBuilder.Application.Services.UserService.Commands.ChangePassword;

namespace PCBuilder.WebApi.Contracts.Users;

public class ChangePasswordRequest:IMapWith<ChangePasswordCommand>
{
    public Guid UserId { get; set; }
    public string Password { get; set; }=String.Empty;
    public string ConfirmPassword { get; set; }=String.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ChangePasswordRequest,ChangePasswordCommand>();
    }
}