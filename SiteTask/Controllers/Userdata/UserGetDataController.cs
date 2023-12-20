using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Model;

namespace SiteTask.Controllers;

public interface IUserGetController
{
    public Task<IActionResult> GetUserId(int id);
}

[Route("api/[controller]")]
[ApiController]
public class UserGetController : ControllerBase, IUserGetController
{
    private ILogger<UserGetController> _logger;
    private string _connect;

    public UserGetController(ILogger<UserGetController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connect = configuration.GetValue<string>("ConnectionStrings");
    }
    
    [HttpGet("userBase/{id:int}")]
    public async Task<IActionResult> GetUserId(int id)
    {
        return Ok();
    }
}