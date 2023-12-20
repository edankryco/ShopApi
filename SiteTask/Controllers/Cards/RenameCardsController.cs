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
    private ILogger<RenameNameController> _logger;
    private readonly string _connect;

    public RenameCardsController(ILogger<RenameNameController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connect = configuration.GetValue<string>("ConnectionStrings");
    }

    [HttpPut("renameCard_Description")]
    public async Task<IActionResult> RenameCardDescription(string renameDescription, int id)
    {
        var mySqlConnect = new MySqlConnection(_connect);
        const string command = "UPDATE CardDataShop SET description = @Description WHERE id = @Id";
        
        await mySqlConnect.OpenAsync();
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        mySqlCommand.Parameters.Add("@Description", MySqlDbType.Text).Value = renameDescription;
        mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        
        await mySqlCommand.ExecuteNonQueryAsync();
        await mySqlConnect.CloseAsync();
        return Ok();
    }
    
    [HttpPut("renameCard_Name")]
    public async Task<IActionResult> RenameCardName(string renameName, int id)
    {
        var mySqlConnect = new MySqlConnection(_connect);
        const string command = "UPDATE CardDataShop SET name = @Name WHERE id = @Id";
        await mySqlConnect.OpenAsync();
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        mySqlCommand.Parameters.Add("@Name", MySqlDbType.Text).Value = renameName;
        mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        await mySqlCommand.ExecuteNonQueryAsync();
        await mySqlConnect.CloseAsync();
        return Ok();
    }
    
    [HttpPut("renameCard_Img")]
    public async Task<IActionResult> RenameCardImg(string renameImg, int id)
    {
        var mySqlConnect = new MySqlConnection(_connect);
        const string command = "UPDATE CardDataShop SET img = @Img WHERE id = @Id";
        await mySqlConnect.OpenAsync();
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        mySqlCommand.Parameters.Add("@Img", MySqlDbType.Text).Value = renameImg;
        mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        await mySqlCommand.ExecuteNonQueryAsync();
        await mySqlConnect.CloseAsync();
        return Ok();
    }
}