using Microsoft.AspNetCore.Mvc;

namespace SiteTask.Controllers.Admin;

[Route("/api/[controller]")]
[ApiController]
public class SeitingsAdminController : ControllerBase
{
    private ILogger<SeitingsAdminController> _logger;
    private IConfiguration _configuration;

    public SeitingsAdminController(IConfiguration configuration, ILogger<SeitingsAdminController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<IActionResult> CreateAdmin()
    {
        const string command = "";
        
        
        return Ok();
    }

    public async Task<IActionResult> DeletedAdmin()
    {
        return Ok();
    }

    public async Task<IActionResult> UpRang()
    {
        return Ok();
    }
}