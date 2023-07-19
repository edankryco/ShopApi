using Microsoft.AspNetCore.Mvc;
using SiteTask.Model;

namespace SiteTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    [HttpGet("authorization")]
    public async Task<IActionResult> UserAddDb(User user)
    {
        user.Name = "edgar"; 
        return Ok();
    }
} 