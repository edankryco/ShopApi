using SiteTask.Controllers.Mail.Send;
using SiteTask.Controllers.TryError;

namespace SiteTask.Controllers.ErrorDistribution;

public interface IErrorDistributionController
{
    public Task GetError(Exception context);
}

public class ErrorDistributionController : IErrorDistributionController
{
    private IErrorServiceMailController _errorServiceMail;

    public ErrorDistributionController(ISendEmailController sendEmailController)
    {
        _errorServiceMail = new ErrorServiceMailController(sendEmailController);
    }

    public async Task GetError(Exception context)
    {
        await _errorServiceMail.PushData(context);
    }
}