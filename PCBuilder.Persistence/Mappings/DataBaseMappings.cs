using AutoMapper;
using PCBuidler.Domain.Models;
using PCBuilder.Persistence.Entities;

namespace PCBuilder.Persistence.Mappings;

public class DataBaseMappings :Profile
{
    public DataBaseMappings()
    {
        CreateMap<UserEntity, User>();
    }
}