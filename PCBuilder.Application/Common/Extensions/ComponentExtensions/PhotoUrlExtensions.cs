using PCBuilder.Core.Interfaces;

namespace PCBuilder.Application.Common.Extensions.ComponentExtensions;

public static class PhotoUrlExtensions
{
    public static bool IsPhotoCustom(this IWithPhotoUrl model)
    {
        var id = model.PhotoUrl.Split("/").Last();
        return string.Equals(id, model.Id.ToString());
    }
}