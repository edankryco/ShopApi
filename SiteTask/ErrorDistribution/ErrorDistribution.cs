using SiteTask.Controllers.Mail.Send;
using SiteTask.Controllers.TelegramMessage;
using SiteTask.Controllers.TryError;

namespace SiteTask.Controllers.ErrorDistribution;

public interface IErrorDistributionController
{
    public Task GetError(Exception context);
}

public class ErrorDistribution : IErrorDistributionController
{
    private IErrorServiceMailController _errorServiceMail;

    public ErrorDistribution(ISendEmailController sendEmailController, ITelegramPostErrors telegramPostErrors)
    {
        _errorServiceMail = new ErrorServiceMailController(sendEmailController, telegramPostErrors);
    }

    public async Task GetError(Exception context)
    {
        await _errorServiceMail.PushData(context);
    }
}