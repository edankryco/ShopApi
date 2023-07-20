using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Prng;
using SiteTask.Model;

namespace SiteTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    string connect = "Server=localhost;port=57137;Database=Click;Uid=root;pwd=root;charset=utf8";

    [HttpPost("authorization_Regist")]
    public async Task<IActionResult> UserRegistration(string name, string mail, string pass, string replace_pass)
    {
        const string command = "INSERT INTO Click(name, mail, pass, replace_pass) VALUES (@Name, @Mail, @Pass, @Replace_pass)";
        var mySqlConnect = new MySqlConnection(connect);
        await mySqlConnect.OpenAsync();
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        mySqlCommand.Parameters.Add("@Name", MySqlDbType.Text).Value = name;
        mySqlCommand.Parameters.Add("@Mail", MySqlDbType.Text).Value = mail;
        mySqlCommand.Parameters.Add("@Pass", MySqlDbType.Text).Value = pass;
        mySqlCommand.Parameters.Add("@Replace_pass", MySqlDbType.Text).Value = replace_pass;
        await mySqlConnect.CloseAsync();
        return Ok();
    }

    [HttpPost("authorization_Login")]
    public async Task<IActionResult> UserLogin(string name, string pass)
    {
        const string command = "SELECT EXISTS(SELECT name, pass FROM Click WHERE name = @Name AND pass = @Pass)";
        var mySqlConnect = new MySqlConnection(connect);
        await mySqlConnect.OpenAsync();
        var mySqlCommand = new MySqlCommand(command, mySqlConnect);
        mySqlCommand.Parameters.Add("@Name", MySqlDbType.Text).Value = name;
        mySqlCommand.Parameters.Add("@Pass", MySqlDbType.Text).Value = pass;
        var exist = mySqlCommand.ExecuteScalar();
        var convertBoolean = Convert.ToBoolean(exist);
        if (!convertBoolean)
        {
            return NoContent();
        }

        return Ok();
    }
}