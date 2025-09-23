namespace PCBuilder.Infrastructure.EmailSender;

public class EmailTokenOptions
{
    public string SecretKey {set;get;} = String.Empty;
    public TimeSpan Lifetime {set;get;} 
}