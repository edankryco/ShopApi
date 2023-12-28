using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Model;

namespace SiteTask.Controllers;

public interface IUserGetController
{
    public Task<IActionResult> GetUserId(int id);
    public Task<IActionResult> GetUsers();
}

[Route("api/[controller]")]
[ApiController]
public class UserGetController : ControllerBase, IUserGetController
{
    private MySqlCommand _mySqlCommand;
    private MySqlConnection _mySqlConnect;
    private ILogger<UserGetController> _logger;
    private string _connect;

    public UserGetController(ILogger<UserGetController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connect = configuration.GetConnectionString("DefaultConnection");
    }
    
    
    
    [HttpGet("userBase/{id:int}")]
    public async Task<IActionResult> GetUserId(int id)
    {
        const string command = "SELECT * FROM DataUser WHERE id = @ID";
        
        return Ok();
    }

    [HttpGet("userBase")]
    public async Task<IActionResult> GetUsers()
    {
        const string command = "SELECT * FROM DataUser";
        
        return Ok();
    }
}