using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers.Userdata;

public interface IDeletingAccountController
{
    public Task<IActionResult> DeletedUserDataId(int id);
    public Task<IActionResult> DeletedUserDataAll();
}

[Route("api/[controller]")]
[ApiController]
public class DeletingAccountController : ControllerBase, IDeletingAccountController
{
    private MySqlCommand _mySqlCommand;
    private MySqlConnection _mySqlConnect;
    private ILogger<DeletingAccountController> _logger;
    private string _connect;

    public DeletingAccountController(ILogger<DeletingAccountController> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _connect = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpDelete("deletedUser/{id:int}")]
    public async Task<IActionResult> DeletedUserDataId(int id)
    {
        const string command = "DELETE FROM Users WHERE id = @ID";
        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        _mySqlCommand.Parameters.Add("@ID", MySqlDbType.Int64).Value = id;

        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnect.CloseAsync();

        return NoContent();
    }

    [HttpDelete("deletedUserAll")]
    public async Task<IActionResult> DeletedUserDataAll()
    {
        Console.Write(_connect);
        const string command = "DELETE FROM Users";

        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();
        
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnect.CloseAsync();

        return NoContent();
    }
}