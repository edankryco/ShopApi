using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Model;

namespace SiteTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    private ILogger<AuthorizationController> _logger;
    private string _connect;

    public AuthorizationController(IConfiguration configuration, ILogger<AuthorizationController> logger)
    {
        _connect = configuration.GetValue<string>("ConnectionStrings");
        _logger = logger;
    }

    [HttpPost("authorization_Regist")]
    public async Task<IActionResult> UserRegistration(string name, string mail, string pass, string replace_pass,
        int balans)
    {
        try
        {
            var user = new User(name, mail, pass, replace_pass, balans);
            var mySqlConnect = new MySqlConnection(_connect);
            await mySqlConnect.OpenAsync();
            Console.WriteLine("connect");
            var command =
                "INSERT INTO Click(name,mail,pass,replace_pass, balans) VALUES (@Name, @Mail, @Pass, @Replace_Pass, @Balans)";
            var sqlCommand = new MySqlCommand(command, mySqlConnect);
            sqlCommand.Parameters.Add("@Name", MySqlDbType.Text).Value = user.Name;
            sqlCommand.Parameters.Add("@Mail", MySqlDbType.Text).Value = user.Mail;
            sqlCommand.Parameters.Add("@Pass", MySqlDbType.Text).Value = user.Pass;
            sqlCommand.Parameters.Add("@Replace_Pass", MySqlDbType.Text).Value = user.ReplacePass;
            sqlCommand.Parameters.Add("@Balans", MySqlDbType.Int64).Value = balans;
            await sqlCommand.ExecuteNonQueryAsync();
            await mySqlConnect.CloseAsync();
            if (user == null)
            {
                return NoContent();
            }

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("authorization_Login")]
    public async Task<IActionResult> UserLogin(string name, string pass)
    {
        try
        {
            const string command = "SELECT EXISTS(SELECT name, pass FROM Click WHERE name = @Name AND pass = @Pass)";
            var mySqlConnect = new MySqlConnection(_connect);
            await mySqlConnect.OpenAsync();
            var mySqlCommand = new MySqlCommand(command, mySqlConnect);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.Text).Value = name;
            mySqlCommand.Parameters.Add("@Pass", MySqlDbType.Text).Value = pass;
            var exist = await mySqlCommand.ExecuteScalarAsync();
            var convertBoolean = Convert.ToBoolean(exist);
            if (!convertBoolean)
            {
                return NoContent();
            }

            await mySqlConnect.CloseAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}