using AutoMapper;

namespace PCBuilder.Application.Common.Mapping;
public interface IMapWith<T>
{
   public void Mapping(Profile profile) =>
       profile.CreateMap(GetType(), typeof(T));
}