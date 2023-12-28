using Microsoft.AspNetCore.Mvc;

namespace SiteTask.Controllers.Admin;

public interface IGetAdminController
{
    
}

[Route("/api/[controller]")]
[ApiController]
public class GetAdminController : ControllerBase,IGetAdminController
{
    
}