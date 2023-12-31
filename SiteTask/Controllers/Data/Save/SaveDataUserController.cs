using Microsoft.AspNetCore.Mvc;

namespace SiteTask.Controllers.Data.Save;

public interface ISaveDataUserController
{
    
}

[Route("/api/[controller]")]
[ApiController]
public class SaveDataUserController : ControllerBase, ISaveDataUserController
{
    
}