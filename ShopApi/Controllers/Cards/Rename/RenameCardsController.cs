using System.Collections;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers.Cards;

public interface IRenameCardsController
{
    public Task<IActionResult> RenameCardDescription(string renameDescription, int id);
    public Task<IActionResult> RenameCardName(string renameName, int id);
    public Task<IActionResult> RenameCardImg(string renameImg, int id);
}

[Route("/api[controller]")]
[ApiController]
public class RenameCardsController : ControllerBase, IRenameCardsController
{
    private MySqlCommand _mySqlCommand;
    private MySqlConnection _mySqlConnect;
    private ILogger<RenameUserController> _logger;
    private readonly string _connect;

    public RenameCardsController(ILogger<RenameUserController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connect = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpPut("renameCard_Description")]
    public async Task<IActionResult> RenameCardDescription(string renameDescription, int id)
    {
        const string command = "UPDATE CardDataShop " +
                               "SET description = @Description " +
                               "WHERE id = @Id";
        
        _mySqlConnect = new MySqlConnection(_connect);
        
        await _mySqlConnect.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        _mySqlCommand.Parameters.Add("@Description", MySqlDbType.Text).Value = renameDescription;
        _mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnect.CloseAsync();
        return NoContent();
    }
    
    [HttpPut("renameCard_Name")]
    public async Task<IActionResult> RenameCardName(string renameName, int id)
    {
        const string command = "UPDATE CardDataShop " +
                               "SET name = @Name " +
                               "WHERE id = @Id";

        
        var mySqlConnect = new MySqlConnection(_connect);
        
        await mySqlConnect.OpenAsync();
        
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        mySqlCommand.Parameters.Add("@Name", MySqlDbType.Text).Value = renameName;
        mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        
        await mySqlCommand.ExecuteNonQueryAsync();
        await mySqlConnect.CloseAsync();
        return NoContent();
    }
    
    [HttpPut("renameCard_Img")]
    public async Task<IActionResult> RenameCardImg(string renameImg, int id)
    {
        const string command = "UPDATE CardDataShop" +
                               " SET img = @Img " +
                               "WHERE id = @Id";

        
        var mySqlConnect = new MySqlConnection(_connect);
        await mySqlConnect.OpenAsync();
        
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        
        mySqlCommand.Parameters.Add("@Img", MySqlDbType.Text).Value = renameImg;
        mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        
        await mySqlCommand.ExecuteNonQueryAsync();
        await mySqlConnect.CloseAsync();
        
        return NoContent();
    }
}