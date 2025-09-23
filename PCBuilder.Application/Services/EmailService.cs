using PCBuilder.Application.Interfaces.Auth;

namespace PCBuilder.Application.Services;

public class EmailService(IEmailSender emailSender, IEmailTokenProvider tokenProvider):IEmailService
{
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IEmailTokenProvider _tokenProvider = tokenProvider;
    
    public async Task SendConfirmEmailAsync(string email,Guid userId)
    {
        var message = @"
        <!DOCTYPE html>
        <html>
        <head>
            <title>Подтверждение регистрации</title>
        </head>
        <body>
          <div style=""font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px; background-color: #f9f9f9;"">
        <h2 style=""text-align: center; color: #4CAF50;"">Почти готово!</h2>
        <p>Привет!</p>
        <p>Мы рады, что вы присоединяетесь к [Название вашего сервиса]. Чтобы начать, пожалуйста, подтвердите свою почту, нажав на кнопку ниже:</p>
        <p style=""text-align: center; margin: 30px 0;"">
            <a href=""{EmailToken}"" style=""background-color: #4CAF50; color: white; padding: 15px 30px; text-decoration: none; border-radius: 8px; font-size: 18px; font-weight: bold; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);"">Подтвердить аккаунт</a>
        </p>
        <p>Кнопка не работает? Скопируйте и вставьте эту ссылку в свой браузер:</p>
        <p style=""font-size: 14px; color: #555; word-wrap: break-word;"">EmailToken</p>
        <p>Если это не вы, просто проигнорируйте это письмо.</p>
        <p>Увидимся,<br>Команда [Название вашего сервиса] 😊</p>
        </body>
        </html>";
        
        string subject = "Confirm your email";
        
        string token= _tokenProvider.GenerateToken(userId);
        
        var confirmationLink = $"https://localhost:5091/confirm_email?emailToken={token}";
            //http://localhost:5090/confirm_email?emailToken=g

        var body = message.Replace("{EmailToken}", confirmationLink);
        await _emailSender.SendEmailAsync(email,subject, body);
    }
}