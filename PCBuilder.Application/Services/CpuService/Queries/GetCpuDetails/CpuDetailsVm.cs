namespace PCBuilder.Application.Services.CpuService.Queries.GetCpuDetails;

public class CpuDetailsVm
{   
    public Guid Id { get; set; }
    public string Name{ get; set;}=string.Empty;
    public string Manufacturer{ get;set; }=string.Empty;
    public string Model{ get;set; }=string.Empty;
    public string Description{get;set;}=string.Empty;
    public string Socket{get;set;}=string.Empty;
    public int? PerformanceCores { get; set;}
    public int? EfficiencyCores { get; set;}
    public string Generation{get;set;}=string.Empty;
    public int Threads{get;set;}
    public string PhotoUrl{get; set; }=string.Empty;
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
}