using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers.Cards;

public interface IDeletedCardController
{
    public Task<IActionResult> DeletedCardShop(string id);
}

[Route("api/[controller]")]
[ApiController]
public class DeletedCardController : ControllerBase, IDeletedCardController
{
    private ILogger<DeletedCardController> _logger;
    private string _connect;

    public DeletedCardController(IConfiguration configuration, ILogger<DeletedCardController> logger)
    {
        _logger = logger;
        _connect = configuration.GetConnectionString("DefaultConnection");
    }


    [HttpDelete("deleted_card")]
    public async Task<IActionResult> DeletedCardShop(string id)
    {
        var mySqlConnect = new MySqlConnection(_connect);
        const string command = "DELETE FROM CardDataShop WHERE id = @Id";
        await mySqlConnect.OpenAsync();
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        
        mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        
        await mySqlCommand.ExecuteNonQueryAsync();
        await mySqlConnect.CloseAsync();
        return Ok();
    }
}