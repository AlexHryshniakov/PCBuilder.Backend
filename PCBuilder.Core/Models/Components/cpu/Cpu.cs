using PCBuilder.Core.Interfaces;

namespace PCBuilder.Core.Models.Components.cpu;

public class Cpu : IWithPhotoUrl
{
    public Guid Id { get; }
    public string Name{ get; set; }
    public string Manufacturer{ get; set; }
    public string Model{ get; set; }
    public string Description{get; set; }
    public string PhotoUrl{get;}
    public Guid SocketId{get; set; }
    public int? PerformanceCores { get; set; }
    public int? EfficiencyCores { get; set;}
    public string Generation{get; set; }
    public int Threads{get; set; }
    public double BaseClockSpeed{get; set; }
    public double? BoostClockSpeed{get; set; }
    public int Tdp { get; set; }
    public bool IntegratedGraphics{get; set; }
    public int L3CacheSize { get; set; }
    public string ProcessNode { get; set; }
    public string PcieVersion { get; set; }
    public string MemoryType { get; set; }
    public int MaxMemorySpeed { get; set; }
    public bool Overclockable { get; set; }
    
    private Cpu(Guid id,string name, string manufacturer, string model,
        string description, string photoUrl, Guid socketId, string generation,
        int threads, double baseClockSpeed, int tdp, bool integratedGraphics,
        int l3CacheSize, string processNode, string pcieVersion, string memoryType,
        int maxMemorySpeed, bool overclockable, double? boostClockSpeed=null,
        int? performanceCores=null,int? efficiencyCores=null,int? cores=null)
    {
        this.Id = id;
        this.Name = name;
        this.Manufacturer = manufacturer;
        this.Model = model;
        this.Description = description;
        this.PhotoUrl = photoUrl;
        this.SocketId = socketId;
        
        if (performanceCores == null && efficiencyCores == null)
        {
              this.PerformanceCores = cores;
              this.EfficiencyCores = 0;
        }
        else
        {
            this.PerformanceCores = performanceCores;
            this.EfficiencyCores = efficiencyCores;
        }
      
        
        this.Generation = generation;
        this.Threads = threads;
        this.BaseClockSpeed = baseClockSpeed;
        this.Tdp = tdp;
        this.IntegratedGraphics = integratedGraphics;
        this.L3CacheSize = l3CacheSize;
        this.ProcessNode = processNode;
        this.PcieVersion = pcieVersion;
        this.MemoryType = memoryType;
        this.MaxMemorySpeed = maxMemorySpeed;
        
        this.Overclockable = overclockable;
        this.BoostClockSpeed = boostClockSpeed;
    }

    public static Cpu Create(Guid id,string name, string manufacturer, string model, 
        string description, string photoUrl, Guid socketId, string generation,
        int threads, double baseClockSpeed, int tdp, bool integratedGraphics,
        int l3CacheSize, string processNode, string pcieVersion, string memoryType,
        int maxMemorySpeed, bool overclockable, double? boostClockSpeed=null,
        int? performanceCores=null,int? efficiencyCores=null, int? cores=null)
    {
        return new Cpu( id,name, manufacturer, model, description,
            photoUrl, socketId, generation, threads, baseClockSpeed, tdp,
            integratedGraphics, l3CacheSize, processNode, pcieVersion, memoryType,
            maxMemorySpeed, overclockable, boostClockSpeed, performanceCores, 
            efficiencyCores,cores);
    }
}