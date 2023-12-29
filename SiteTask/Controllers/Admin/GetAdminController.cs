using Microsoft.AspNetCore.Mvc;

namespace SiteTask.Controllers.Admin;

public interface IGetAdminController
{
    public Task<IActionResult> GetAdminId();
    public Task<IActionResult> GetAdminAll();
}

[Route("/api/[controller]")]
[ApiController]
public class GetAdminController : ControllerBase,IGetAdminController
{
    [HttpGet("getAdmin/{id:int}")]
    public async Task<IActionResult> GetAdminId()
    {
        return Ok();
    }

    [HttpGet("getAdmin")]
    public async Task<IActionResult> GetAdminAll()
    {
        return Ok();
    }
}