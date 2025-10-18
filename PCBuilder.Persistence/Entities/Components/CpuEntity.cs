using PCBuilder.Persistence.Entities.ItemProperties;

namespace PCBuilder.Persistence.Entities.Components;

public class CpuEntity
{
    public Guid Id { get; set; }
    public string Name{ get; set;}=string.Empty;
    public string Manufacturer{ get;set; }=string.Empty;
    public string Model{ get;set; }=string.Empty;
    public string Description{get;set;}=string.Empty;
    public string PhotoUrl{get;set;}=string.Empty;
    public Guid SocketId{get;set;}
    public int? PerformanceCores { get; set;}
    public int? EfficiencyCores { get; set;}
    public string Generation{get;set;}=string.Empty;
    public int Threads{get;set;}
    public double BaseClockSpeed{get;set;}
    public double? BoostClockSpeed{get;set;}
    public int Tdp { get;set;}
    public bool IntegratedGraphics{get;set;}
    public int L3CacheSize { get;set; }
    public string ProcessNode { get; set;}=string.Empty;
    public string PcieVersion { get;set; }=string.Empty;
    public string MemoryType { get;set; }=string.Empty;
    public int MaxMemorySpeed { get; set;}
    public bool Overclockable { get; set;}

    public SocketEntity Socket { get; set; } 
}