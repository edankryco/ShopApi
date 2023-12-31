using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers.Cards;

public interface IDeletedCardController
{
    public Task<IActionResult> DeletedCardShopId(int id);
    public Task<IActionResult> DeletedCardsShopAll();
}

[Route("api/[controller]")]
[ApiController]
public class DeletedCardController : ControllerBase, IDeletedCardController
{
    private MySqlCommand _mySqlCommand;
    private MySqlConnection _mySqlConnect;
    private ILogger<DeletedCardController> _logger;
    private string _connect;

    public DeletedCardController(IConfiguration configuration, ILogger<DeletedCardController> logger)
    {
        _logger = logger;
        _connect = configuration.GetConnectionString("DefaultConnection");
    }


    [HttpDelete("deleted_card{id:int}")]
    public async Task<IActionResult> DeletedCardShopId(int id)
    {
        const string command = "DELETE FROM CardDataShop WHERE id = @Id";

        
        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        
        _mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnect.CloseAsync();
        return NoContent();
    }

    [HttpDelete("deleted_card/All")]
    public async Task<IActionResult> DeletedCardsShopAll()
    {
        const string command = "DELETE FROM CardDataShop";

        
        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();
        
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnect.CloseAsync();
        
        return NoContent();
    }
}