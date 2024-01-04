using MailKit.Net.Smtp;
using MimeKit;

namespace SiteTask.Controllers.Mail.Send;

public interface ISendEmailController
{
    public Task SentMailMessage(string email, string subject, string message);
}

public class SendEmailController : ISendEmailController
{
    public async Task SentMailMessage(string email, string subject, string message)
    {
        var mimeMessage = new MimeMessage();
        
        mimeMessage.From.Add(new MailboxAddress("Администрация сайта", "edankryzo@yandex.ru"));
        mimeMessage.To.Add(new MailboxAddress("", email));
        mimeMessage.Subject = subject;
        mimeMessage.Body = new TextPart()
        {
            Text = message
        };

        var client = new SmtpClient();

        await client.ConnectAsync("smtp.yandex.ru", 25, false);
        await client.AuthenticateAsync("edankryzo@yandex.ru", "ymxwmgwjaaenthjb");
        await client.SendAsync(mimeMessage);
        
        await client.DisconnectAsync(true);
    }
}