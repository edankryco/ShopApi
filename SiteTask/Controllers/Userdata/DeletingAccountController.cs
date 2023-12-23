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
    private ILogger<DeletingAccountController> _logger;
    private string _connect;

    public DeletingAccountController(ILogger<DeletingAccountController> logger, string connect,
        IConfiguration configuration)
    {
        _logger = logger;
        _connect = configuration.GetConnectionString("ConnectionStrings");
    }

    [HttpDelete("deleted_User/{id:int}")]
    public async Task<IActionResult> DeletedUserData(int id)
    {
        const string command = "DELETE FROM Click WHERE id = @Id";
        var mySqlConnect = new MySqlConnection(_connect);
        await mySqlConnect.OpenAsync();
        
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        
        await mySqlCommand.ExecuteNonQueryAsync();
        await mySqlConnect.CloseAsync();
        
        return Ok();
    }
}