using AutoMapper;
using PCBuilder.Core.Models.Components.cpu;
using PCBuilder.Core.Models.Users;
using PCBuilder.Persistence.Entities.Components;
using PCBuilder.Persistence.Entities.User;

namespace PCBuilder.Persistence.Mappings;

public class DataBaseMappings :Profile
{
    public DataBaseMappings()
    {
        CreateMap<UserEntity, User>();
        CreateMap<RefreshTokenEntity, RefreshToken>();
        CreateMap<EmailTokensEntity, EmailTokens>();
        CreateMap<CpuEntity, Cpu>();
    }
}