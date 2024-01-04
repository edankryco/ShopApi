using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Controllers.TryError;
using SiteTask.Controllers.ValidationData;
using SiteTask.Create;
using SiteTask.Model;
using SiteTask.Model.HashPasswordModel;

namespace SiteTask.Controllers;

public interface IAuthorizationController
{
    public Task<IActionResult> UserRegistration(User user);
    public Task<IActionResult> UserLogin(string login, Password pass);
}

[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase, IAuthorizationController
{
    private ILogger<AuthorizationController> _logger;
    private string _connect;

    private readonly MySqlConnection _mySqlConnection;
    private MySqlCommand _mySqlCommand;

    private IValidationController<string> _validationUsers;

    public AuthorizationController(IConfiguration configuration, ILogger<AuthorizationController> logger)
    {
        _connect = configuration.GetConnectionString("DefaultConnection");
        _validationUsers = new ValidationController<string>(configuration);
        _logger = logger;
    }

    [HttpPost("ifTableNo")]
    public async Task IfTableNo()
    {
        var create = new CreateTable(_connect);
        await create.StartSearch();
    }

    [HttpPost("authorization/Regist")]
    public async Task<IActionResult> UserRegistration(User user)
    {
        await IfTableNo();

        var isEmptyUser = _validationUsers.SearchData
            (user.Login, "Users", "login");
        if (isEmptyUser.Result)
            return NoContent();

        const string command = "INSERT INTO Users" +
                               "(login, name, age, email,password,repassword, balanc)" +
                               " VALUES(" +
                               "@Login, @Name, @Age, @Mail, " +
                               "@Pass, @Replace_Pass, @Balanc)";

        var mySqlConnect = new MySqlConnection(_connect);
        await mySqlConnect.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, mySqlConnect);

        _mySqlCommand.Parameters.Add("@Login", MySqlDbType.VarChar).Value = user.Login;
        _mySqlCommand.Parameters.Add("@Name", MySqlDbType.VarChar).Value = user.Name;
        _mySqlCommand.Parameters.Add("@Age", MySqlDbType.Int64).Value = user.Age;
        _mySqlCommand.Parameters.Add("@Mail", MySqlDbType.VarChar).Value = user.Mail;
        _mySqlCommand.Parameters.Add("@Pass", MySqlDbType.Int64).Value = user.Pass.HashPass();
        _mySqlCommand.Parameters.Add("@Replace_Pass", MySqlDbType.Int64).Value = user.ReplacePass.HashPass();
        _mySqlCommand.Parameters.Add("@Balanc", MySqlDbType.Int64).Value = user.Balanc;

        await _mySqlCommand.ExecuteNonQueryAsync();
        await mySqlConnect.CloseAsync();

        return Ok();
    }

    [HttpPost("authorization/Login")]
    public async Task<IActionResult> UserLogin(string login, Password pass)
    {
        const string command = "SELECT EXISTS" +
                               "(SELECT login, password FROM " +
                               "Users WHERE login = @Login " +
                               "AND password = @Pass)";

        var mySqlConnect = new MySqlConnection(_connect);
        await mySqlConnect.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, mySqlConnect);

        _mySqlCommand.Parameters.Add("@Login", MySqlDbType.Text).Value = login;
        _mySqlCommand.Parameters.Add("@Pass", MySqlDbType.Text).Value = pass.HashPass();

        var exist = await _mySqlCommand.ExecuteScalarAsync();
        var convertBoolean = Convert.ToBoolean(exist);

        if (convertBoolean)
        {
            return Ok();
        }

        await mySqlConnect.CloseAsync();

        return NoContent();
    }
}