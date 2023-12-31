using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers;

public interface IRenameNameController
{
    public Task<IActionResult> RenameUser(string login, string name);
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

    [HttpPut("renameName/{login}")]
    public async Task<IActionResult> RenameUser(string login, string name)
    {
        const string command = "UPDATE Users " +
                               "SET name = @Name " +
                               "WHERE login = @Login";
        
        _mySqlConnect = new MySqlConnection(_connect);
        
        await _mySqlConnect.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        _mySqlCommand.Parameters.Add("@Name", MySqlDbType.Text).Value = name;
        _mySqlCommand.Parameters.Add("@Login", MySqlDbType.VarChar).Value = login;
        
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnect.CloseAsync();
        
        return NoContent();
    }
}