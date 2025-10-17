namespace PCBuilder.Core.Shared.Email;

public class EmailTokenOptions
{
    public string SecretKey {set;get;} = String.Empty;
    public int ConfirmTokenLifetimeInHours {set;get;} 
    public int ResetPasswordTokenLifetimeInMinutes {set;get;} 
}