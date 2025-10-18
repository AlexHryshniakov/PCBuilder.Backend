using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.Application.Common.Mapping;
using PCBuilder.Application.Services.CpuService.Commands.CreateCpu;

namespace PCBuilder.API.Contracts.Cpus;

public record CreateCpuRequest:IMapWith<CreateCpuCommand>
{
    [Required] public string Name{ get; set;}=string.Empty;
    [Required] public string Manufacturer{ get;set; }=string.Empty;
    [Required] public string Model{ get;set; }=string.Empty;
    [Required] public string Description{get;set;}=string.Empty;
    [Required] public Guid SocketId{get;set;}
    [Required] public int? PerformanceCores { get; set;}
    public int? EfficiencyCores { get; set;}
    [Required] public string Generation{get;set;}=string.Empty;
    [Required] public int Threads{get;set;}
    [Required] public double BaseClockSpeed{get;set;}
    [Required] public double? BoostClockSpeed{get;set;}
    [Required] public int Tdp { get;set;}
    [Required] public bool IntegratedGraphics{get;set;}
    [Required] public int L3CacheSize { get;set; }
    [Required] public string ProcessNode { get; set;}=string.Empty;
    [Required] public string PcieVersion { get;set; }=string.Empty;
    [Required] public string MemoryType { get;set; }=string.Empty;
    [Required] public int MaxMemorySpeed { get; set;}
    [Required] public bool Overclockable { get; set;}
    [FromForm] public IFormFile Photo { get; set; } = null!;
    
    
    public void Mapping(Profile profile)
        => profile.CreateMap<CreateCpuRequest,CreateCpuCommand>()
            .ForMember(command=>command.PhotoStream,opt
                =>opt.MapFrom(request=>request.Photo != null ? request.Photo.OpenReadStream() : Stream.Null))
            .ForMember(command=>command.ContentType,opt
                =>opt.MapFrom(request=>request.Photo != null ? request.Photo.ContentType : string.Empty));
}