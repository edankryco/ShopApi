using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeletingAccountController : ControllerBase
{
    string connect = "Server=localhost;port=63570;Database=Click;Uid=root;pwd=root;charset=utf8";

    [HttpDelete("deleted_User/{id:int}")]
    public async Task<IActionResult> DeletedUserData(int id)
    {
        var mySqlConnect = new MySqlConnection(connect);
        const string command = "DELETE FROM Click WHERE id = @Id";
        await mySqlConnect.OpenAsync();
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        await mySqlConnect.CloseAsync();
        return Ok();
    }
}