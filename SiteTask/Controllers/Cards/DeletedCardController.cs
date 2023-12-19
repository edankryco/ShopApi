using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers.Cards;

[Route("api/[controller]")]
[ApiController]
public class DeletedCardController : ControllerBase
{
    private ILogger<DeletedCardController> _logger;
    private string _connect;

    public DeletedCardController(IConfiguration configuration, ILogger<DeletedCardController> logger)
    {
        _logger = logger;
        _connect = configuration.GetValue<string>("CConnectionStrings");
        ;
    }


    [HttpDelete("deleted_card")]
    public async Task<IActionResult> DeletedCardShop(string Id)
    {
        var mySqlConnect = new MySqlConnection(_connect);
        var command = "DELETE FROM CardDataShop WHERE id = @Id";
        await mySqlConnect.OpenAsync();
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = Id;
        await mySqlCommand.ExecuteNonQueryAsync();
        await mySqlConnect.CloseAsync();
        return Ok();
    }
}