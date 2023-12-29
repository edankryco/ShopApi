using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers;

public interface IRenameNameController
{
    public Task<IActionResult> RenameUser(int id, string name);
}

[Route("api/[controller]")]
[ApiController]
public class RenameUserController : ControllerBase, IRenameNameController
{
    private MySqlCommand _mySqlCommand;
    private MySqlConnection _mySqlConnect;
    private ILogger<RenameUserController> _logger;
    private string _connect;

    public RenameUserController(ILogger<RenameUserController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connect = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpPut("rename_Name/{id:int}")]
    public async Task<IActionResult> RenameUser(int id, string name)
    {
        _mySqlConnect = new MySqlConnection(_connect);
        
        await _mySqlConnect.OpenAsync();
        const string command = "UPDATE Click SET name = @Name WHERE id = @Id";
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        _mySqlCommand.Parameters.Add("@Name", MySqlDbType.Text).Value = name;
        _mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnect.CloseAsync();
        
        return NoContent();
    }
}