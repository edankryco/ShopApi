using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers.Userdata;

public interface IDeletingAccountController
{
    public Task<IActionResult> DeletedUserData(int id);
}

[Route("api/[controller]")]
[ApiController]
public class DeletingAccountController : ControllerBase, IDeletingAccountController
{
    private MySqlCommand _mySqlCommand = new();
    private MySqlConnection _mySqlConnect = new();
    private ILogger<DeletingAccountController> _logger;
    private string _connect;

    public DeletingAccountController(ILogger<DeletingAccountController> logger, string connect,
        IConfiguration configuration)
    {
        _logger = logger;
        _connect = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpDelete("deleted_User/{id:int}")]
    public async Task<IActionResult> DeletedUserData(int id)
    {
        const string command = "DELETE FROM Click WHERE id = @Id";
        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        _mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;

        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnect.CloseAsync();

        return Ok();
    }
}