using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

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

    [HttpPut("rename_Name/{id:int}")]
    public async Task<IActionResult> RenameUser(int id, string name)
    {
        var mySqlConnect = new MySqlConnection(connect);
        await mySqlConnect.OpenAsync();
        var command = "UPDATE Click SET name = @Name WHERE id = @Id";
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        mySqlCommand.Parameters.Add("@Name", MySqlDbType.Text).Value = name;
        mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        return Ok();
    }
}