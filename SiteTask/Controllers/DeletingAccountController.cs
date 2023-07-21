using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeletingAccountController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<DeletingAccountController> _logger;

    public DeletingAccountController(ILogger<DeletingAccountController> logger)
    {
        _logger = logger;
    }
    
    string connect = "Server=localhost;port=51363;Database=Click;Uid=root;pwd=root;charset=utf8";

    [HttpDelete("deleted_User/{id:int}")]
    public async Task<IActionResult> DeletedUserData(int id)
    {
        const string command = "DELETE FROM Click WHERE id = @Id";
        var mySqlConnect = new MySqlConnection(connect);
        await mySqlConnect.OpenAsync();
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        mySqlCommand.ExecuteNonQuery();
        await mySqlConnect.CloseAsync();
        return Ok();
    }
}