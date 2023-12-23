using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Model;

namespace SiteTask.Controllers;

public interface IAuthorizationController
{
    public Task<IActionResult> UserRegistration(User user);
    public Task<IActionResult> UserLogin(User user);
}

[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase, IAuthorizationController
{
    private ILogger<AuthorizationController> _logger;
    private string _connect;
    
    private MySqlConnection mySqlConnection = new();
    private MySqlCommand mySqlCommand = new();
    private const string connect = "Server=mysql.students.it-college.ru;Database=i22s0909;Uid=i22s0909;pwd=5x9PVV83;charset=utf8";

    public AuthorizationController(IConfiguration configuration, ILogger<AuthorizationController> logger)
    {
        _connect = configuration.GetConnectionString("DefaultConnection");
        _logger = logger;
    }

    public async Task IfTableNo()
    {
        var create = new CreateTable.CreateTable(mySqlCommand, mySqlConnection, connect);
        create.StartSearch();
    }

    [HttpPost("authorization_Regist")]
    public async Task<IActionResult> UserRegistration(User user)
    {

        var mySqlConnect = new MySqlConnection(_connect);
        await mySqlConnect.OpenAsync();

        const string command = "INSERT INTO Click" +
                               "(name,mail,pass,replace_pass, balans)" +
                               " VALUES (@Name, @Mail, @Pass, @Replace_Pass, @Balans)";
        
        
        var sqlCommand = new MySqlCommand(command, mySqlConnect);
        
        sqlCommand.Parameters.Add("@Name", MySqlDbType.VarChar).Value = user.Name;
        sqlCommand.Parameters.Add("@Mail", MySqlDbType.VarChar).Value = user.Mail;
        sqlCommand.Parameters.Add("@Pass", MySqlDbType.VarChar).Value = user.Pass.HashPass();
        sqlCommand.Parameters.Add("@Replace_Pass", MySqlDbType.VarChar).Value = user.ReplacePass.HashPass();
        sqlCommand.Parameters.Add("@Balans", MySqlDbType.Int64).Value = user.Balans;

        await sqlCommand.ExecuteNonQueryAsync();
        await mySqlConnect.CloseAsync();

        return Ok();
    }

    [HttpPost("authorization_Login")]
    public async Task<IActionResult> UserLogin(User user)
    {
        try
        {
            const string command = "SELECT EXISTS" +
                                   "(SELECT name, pass FROM " +
                                   "Click WHERE name = @Name AND pass = @Pass)";

            var mySqlConnect = new MySqlConnection(_connect);
            await mySqlConnect.OpenAsync();
            var mySqlCommand = new MySqlCommand(command, mySqlConnect);

            mySqlCommand.Parameters.Add("@Name", MySqlDbType.Text).Value = user.Name;
            mySqlCommand.Parameters.Add("@Pass", MySqlDbType.Text).Value = user.Pass;

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