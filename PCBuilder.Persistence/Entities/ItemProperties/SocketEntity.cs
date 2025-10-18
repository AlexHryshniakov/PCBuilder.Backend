using PCBuilder.Persistence.Entities.Components;

namespace PCBuilder.Persistence.Entities.ItemProperties
{
    public class SocketEntity
    {
        public Guid Id { get; set; }
        public string Name{ get; set; }=String.Empty;
    
        public ICollection<CpuEntity> Cpus { get; set; } = new List<CpuEntity>();
    }
}