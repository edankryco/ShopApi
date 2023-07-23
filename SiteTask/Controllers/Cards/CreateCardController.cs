using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers.Cards;

[Route("api/[controller]")]
[ApiController]
public class CreateCardController : ControllerBase
{
    string connect = "Server=localhost;port=60341;Database=CardDataShop;Uid=root;pwd=root;charset=utf8";
    
    [HttpPost("create_card")]
    public async Task<IActionResult> PostCardsDara(string name, string img, string description)
    {
        var cards = new Model.Cards(name, img, description);
        var mySqlConnect = new MySqlConnection(connect);
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