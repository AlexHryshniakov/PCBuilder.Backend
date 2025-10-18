namespace PCBuilder.Core.Models.Components.cpu;

public class Cpu
{
    public Guid Id { get; }
    public string Name{ get; }
    public string Manufacturer{ get; }
    public string Model{ get; }
    public string Description{get;}
    public string PhotoUrl{get;}
    public Guid SocketId{get;}
    public int? PerformanceCores { get; }
    public int? EfficiencyCores { get; }
    public string Generation{get;}
    public int Threads{get;}
    public double BaseClockSpeed{get;}
    public double? BoostClockSpeed{get;}
    public int Tdp { get;}
    public bool IntegratedGraphics{get;}
    public int L3CacheSize { get; }
    public string ProcessNode { get; }
    public string PcieVersion { get; }
    public string MemoryType { get; }
    public int MaxMemorySpeed { get; }
    public bool Overclockable { get; }
    
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