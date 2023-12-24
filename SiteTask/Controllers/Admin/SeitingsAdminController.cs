using Microsoft.AspNetCore.Mvc;

namespace SiteTask.Controllers.Admin;

public interface ISeitingsAdminController
{
    public Task<IActionResult> CreateAdmin();
    public Task<IActionResult> DeletedAdmin();
    public Task<IActionResult> UpRang();
}

[Route("/api/[controller]")]
[ApiController]
public class SeitingsAdminController : ControllerBase, ISeitingsAdminController
{
    private ILogger<SeitingsAdminController> _logger;
    private IConfiguration _configuration;

    public SeitingsAdminController(IConfiguration configuration, ILogger<SeitingsAdminController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost("createadmin")]
    public async Task<IActionResult> CreateAdmin()
    {
        const string command = "";
        
        
        return Ok();
    }

    [HttpPost("deletedadmin")]
    public async Task<IActionResult> DeletedAdmin()
    {
        return Ok();
    }

    [HttpPost("uprang")]
    public async Task<IActionResult> UpRang()
    {
        return Ok();
    }
}