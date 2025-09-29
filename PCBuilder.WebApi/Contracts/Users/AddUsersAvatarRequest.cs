using Microsoft.AspNetCore.Mvc;

namespace PCBuilder.WebApi.Contracts.Users;

public record AddUsersAvatarRequest([FromForm] IFormFile Avatar);