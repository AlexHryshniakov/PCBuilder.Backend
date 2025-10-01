using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PCBuilder.Application.Common.Mapping;
using PCBuilder.Application.Services.UserService.Commands.LoginUser;

namespace PCBuilder.WebApi.Contracts.Users;

public record LoginUserRequest: IMapWith<LoginUserCommand>
{
    [Required] public string Email { get; set; } = null!;
    [Required] public string Password { get; set; }= null!;

    public void Mapping(Profile profile)
        => profile.CreateMap<LoginUserRequest,LoginUserCommand>();
}