using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers;

public interface IRenameNameController
{
    public Task<IActionResult> RenameUser(int id, string name);
}

[Route("api/[controller]")]
[ApiController]
public class RenameNameController : ControllerBase, IRenameNameController
{
    private ILogger<RenameNameController> _logger;
    private string _connect;

    public RenameNameController(ILogger<RenameNameController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connect = configuration.GetValue<string>("ConnectionStrings");
    }

    [HttpPut("rename_Name/{id:int}")]
    public async Task<IActionResult> RenameUser(int id, string name)
    {
        var mySqlConnect = new MySqlConnection(_connect);
        
        await mySqlConnect.OpenAsync();
        const string command = "UPDATE Click SET name = @Name WHERE id = @Id";
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        mySqlCommand.Parameters.Add("@Name", MySqlDbType.Text).Value = name;
        mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        
        await mySqlCommand.ExecuteNonQueryAsync();
        await mySqlConnect.CloseAsync();
        
        return Ok();
    }
}