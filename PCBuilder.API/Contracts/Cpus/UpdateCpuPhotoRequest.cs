using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.Application.Common.Mapping;
using PCBuilder.Application.Services.CpuService.Commands.UpdateCpuPhoto;

namespace PCBuilder.API.Contracts.Cpus;

public class UpdateCpuPhotoRequest :IMapWith<UpdateCpuPhotoCommand>
{
    public Guid CpuId{get;set;}
    [FromForm] public IFormFile Photo { get; set; } = null!;
    
    public void Mapping(Profile profile)
        => profile.CreateMap<UpdateCpuPhotoRequest,UpdateCpuPhotoCommand>()
            .ForMember(command=>command.PhotoStream,opt
                =>opt.MapFrom(request=>request.Photo != null ? request.Photo.OpenReadStream() : Stream.Null))
            .ForMember(command=>command.ContentType,opt
                =>opt.MapFrom(request=>request.Photo != null ? request.Photo.ContentType : string.Empty));
}