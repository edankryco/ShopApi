using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers.Cards;

public interface ICreateCardController
{
    public Task<IActionResult> CreateCardsData(Model.Cards cards);
}

[Route("api/[controller]")]
[ApiController]
public class CreateCardController : ControllerBase, ICreateCardController
{
    private MySqlCommand _mySqlCommand;
    private MySqlConnection _mySqlConnect;
    private ILogger<CreateCardController> _logger;
    readonly string _connect;

    public CreateCardController(IConfiguration configuration, ILogger<CreateCardController> logger)
    {
        _logger = logger;
        _connect = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpPost("createcard")]
    public async Task<IActionResult> CreateCardsData(Model.Cards cards)
    {
        const string command = "INSERT INTO CardDataShop" +
                               "(namecards,img, iduser, description) " +
                               "VALUES (@Name, @Img, @IdUser, @Description)";

        
        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        
        _mySqlCommand.Parameters.Add("@Name", MySqlDbType.VarChar).Value = cards.Name;
        _mySqlCommand.Parameters.Add("@Img", MySqlDbType.Blob).Value = cards.Img = await System.IO.File.
            ReadAllBytesAsync("../Static/Img/no-profile.png");
        _mySqlCommand.Parameters.Add("@IdUser", MySqlDbType.Int64).Value = cards.IdUser;
        _mySqlCommand.Parameters.Add("@Description", MySqlDbType.Text).Value = cards.Description;

        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnect.CloseAsync();

        return NoContent();
    }
}