using MediatR;

namespace PCBuilder.Application.Services.CpuService.Commands.CreateCpu;

public class CreateCpuCommand :IRequest,IAsyncDisposable
{
    public string Name{ get; set;}=string.Empty;
    public string Manufacturer{ get;set; }=string.Empty;
    public string Model{ get;set; }=string.Empty;
    public string Description{get;set;}=string.Empty;
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
    public Stream PhotoStream { get; set; } = Stream.Null;
    public string ContentType { get; set; } = null!;


    public async ValueTask DisposeAsync()
    {
        if (PhotoStream != Stream.Null)
            await PhotoStream.DisposeAsync().ConfigureAwait(false);
    }
}