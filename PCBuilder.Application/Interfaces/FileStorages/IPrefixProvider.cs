namespace PCBuilder.Application.Interfaces.FileStorages;

public interface IPrefixProvider
{
    public string GetObjectPath(string path, string id);
}