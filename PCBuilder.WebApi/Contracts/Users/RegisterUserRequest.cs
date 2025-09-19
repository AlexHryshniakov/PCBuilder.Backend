using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PCBuilder.Application.Common.Mapping;
using PCBuilder.Application.Services.UserService.Command.CreateUser;

namespace PCBuilder.WebApi.Contracts.Users;

public record RegisterUserRequest : IMapWith<CreateUserCommand>
{
    [Required] public string UserName{ get; set; } = null!;
    [Required] public string Password { get; set; }= null!;
    [Required] public string Email{ get; set; } = null!;

    public void Mapping(Profile profile)
        => profile.CreateMap<RegisterUserRequest,CreateUserCommand>();
}