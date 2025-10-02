using PCBuidler.Domain.Enums;

namespace PCBuilder.Application.Interfaces.Mail;

public interface IEmailTokenProvider
{
    string GenerateToken(Guid userId);
}