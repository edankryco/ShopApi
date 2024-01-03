namespace SiteTask.Controllers.TryError;

public interface IErrorServiceMailController
{
    public void PushData(string error);
}

public class ErrorServiceMailController : IErrorServiceMailController
{
    public void PushData(string error)
    {
        throw new NotImplementedException();
    }
}