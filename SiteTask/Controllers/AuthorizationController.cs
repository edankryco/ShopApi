using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Prng;
using SiteTask.Model;

namespace SiteTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    [HttpPost("authorization_Regist")]
    public async Task<IActionResult> UserRegistration(User user)
    {
        return Ok();
    }

    [HttpPost()]
    public async Task<IActionResult> UserLogin(User user)
    {
        return Ok();
    }
} 