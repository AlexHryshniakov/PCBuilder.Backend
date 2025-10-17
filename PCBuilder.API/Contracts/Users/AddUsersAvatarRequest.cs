using Microsoft.AspNetCore.Mvc;

namespace PCBuilder.API.Contracts.Users;

public record AddUsersAvatarRequest([FromForm] IFormFile Avatar);