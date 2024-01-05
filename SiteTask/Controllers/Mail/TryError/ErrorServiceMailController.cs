using SiteTask.Controllers.Mail.Send;
using SiteTask.Controllers.TelegramMessage;

namespace SiteTask.Controllers.TryError;

public interface IErrorServiceMailController
{
    public Task PushData(Exception error);
}

public class ErrorServiceMailController : IErrorServiceMailController
{
    private readonly ISendEmailController _sendEmailController;
    private ITelegramPostErrors _telegramPostErrors;

    public ErrorServiceMailController(ISendEmailController sendEmailController, ITelegramPostErrors telegramPostErrors)
    {
        _sendEmailController = sendEmailController;
        _telegramPostErrors = telegramPostErrors;
    }

    public async Task PushData(Exception error)
    {
        await _sendEmailController.SentMailMessage("edankryzo66@yandex.com", "Капец ошибка", error.ToString());
        await _telegramPostErrors.PostErrors(error.ToString(), "", "", "");
    }
}