using Microsoft.AspNetCore.Mvc;

namespace SiteTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RenameNameController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<RenameNameController> _logger;

    public RenameNameController(ILogger<RenameNameController> logger)
    {
        _logger = logger;
    }
    
    string connect = "Server=localhost;port=51363;Database=Click;Uid=root;pwd=root;charset=utf8";

    [HttpPut("")]
    public async Task<IActionResult> RenameUser()
    {
        return Ok();
    }
}