using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers.Cards;

[Route("api/[controller]")]
[ApiController]
public class DeletedCardController : ControllerBase
{
    private string connect = "Server=localhost;port=60341;Database=CardDataShop;Uid=root;pwd=root;charset=utf8";

    [HttpDelete("deleted_card")]
    public async Task<IActionResult> DeletedCardShop(string Id)
    {
        var mySqlConnect = new MySqlConnection(connect);
        var command = "DELETE FROM CardDataShop WHERE id = @Id";
        await mySqlConnect.OpenAsync();
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = Id;
        await mySqlCommand.ExecuteNonQueryAsync();
        await mySqlConnect.CloseAsync();
        return Ok();
    }
}