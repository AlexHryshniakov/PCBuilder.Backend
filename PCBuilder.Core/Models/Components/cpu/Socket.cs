namespace PCBuilder.Core.Models.Components.cpu;

public class Socket
{
    public Guid Id { get; }
    public string Name{ get; }

    private Socket(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    
    public static Socket Create(string name)
    {
        return new Socket(name);
    }
}