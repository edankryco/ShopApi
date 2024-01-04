using SiteTask.Controllers.Mail.Send;

namespace SiteTask.Controllers.TryError;

public interface IErrorServiceMailController
{
    public Task PushData(Exception error);
}

public class ErrorServiceMailController : IErrorServiceMailController
{
    private ISendEmailController _sendEmailController;

    public ErrorServiceMailController(ISendEmailController sendEmailController)
    {
        _sendEmailController = sendEmailController;
    }

    public async Task PushData(Exception error)
    {
        await _sendEmailController.SentMailMessage("edankryzo66@yandex.com", "Капец ошибка", error.ToString());
    }
}