using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers.Cards;

public interface ICreateCardController
{
    public Task<IActionResult> PostCardsData(Model.Cards cards);
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

    [HttpPost("create_card")]
    public async Task<IActionResult> PostCardsData(Model.Cards cards)
    {
        _mySqlConnect = new MySqlConnection(_connect);
        const string command = "INSERT INTO CardDataShop(name,img,description) VALUES (@Name, @Img, @Description)";
        await _mySqlConnect.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        
        _mySqlCommand.Parameters.Add("@Name", MySqlDbType.Text).Value = cards.Name;
        _mySqlCommand.Parameters.Add("@Img", MySqlDbType.Text).Value = cards.Img;
        _mySqlCommand.Parameters.Add("@Description", MySqlDbType.Text).Value = cards.Description;

        await _mySqlCommand.ExecuteScalarAsync();
        await _mySqlConnect.CloseAsync();

        return Ok();
    }
}