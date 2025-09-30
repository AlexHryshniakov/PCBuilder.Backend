namespace PCBuidler.Domain.Models;

public class User
{
    public Guid Id { get; }
    public string UserName{ get; }
    public string Email{ get; }
    public string PasswordHash{ get; }
    
    public bool EmailConfirmed { private set; get; }
    public string AvatarUrl{ set; get; }

    private User(Guid id, string userName, string email, string passwordHash, string avatarUrl)
    {
        Id = id;
        UserName = userName;
        PasswordHash = passwordHash;
        Email = email;
        EmailConfirmed = false;
        AvatarUrl =avatarUrl;
    }

    public void ConfirmEmail()
    {
        EmailConfirmed = true;
    }
    
    public static User Create(Guid id, string userName, string email, string passwordHash, string avatarUrl)
    {
        return new User(id, userName, email, passwordHash,avatarUrl);
    }
}