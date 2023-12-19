using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Model;

namespace SiteTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserGetController : ControllerBase
{
    private ILogger<UserGetController> _logger;
    private string _connect;

    public UserGetController(ILogger<UserGetController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connect = configuration.GetValue<string>("ConnectionStrings");
    }

    [HttpGet("userBase")]
    public async Task<IActionResult> GetUser()
    {
        
    }

    [HttpGet("userBase/{id:int}")]
    public async Task<IActionResult> GetUserId(int id)
    {
        
    }
}