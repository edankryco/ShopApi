using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers.Cards;

[Route("api/[controller]")]
[ApiController]
public class CreateCardController : ControllerBase
{
    private ILogger<CreateCardController> _logger;
    readonly string _connect = "Server=localhost;port=60341;Database=CardDataShop;Uid=root;pwd=root;charset=utf8";

    public CreateCardController(IConfiguration configuration, ILogger<CreateCardController> logger)
    {
        _logger = logger;
        _connect = configuration.GetValue<string>("ConnectionStrings");
    }

    [HttpPost("create_card")]
    public async Task<IActionResult> PostCardsData(string name, string img, string description)
    {
        var cards = new Model.Cards(name, img, description);
        var mySqlConnect = new MySqlConnection(_connect);
        const string command = "INSERT INTO CardDataShop(name,img,description) VALUES (@Name, @Img, @Description)";
        await mySqlConnect.OpenAsync();
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        mySqlCommand.Parameters.Add("@Name", MySqlDbType.Text).Value = cards.Name;
        mySqlCommand.Parameters.Add("@Img", MySqlDbType.Text).Value = cards.Img;
        mySqlCommand.Parameters.Add("@Description", MySqlDbType.Text).Value = cards.Description;

        await mySqlCommand.ExecuteScalarAsync();
        await mySqlConnect.CloseAsync();

        return Ok();
    }
}