using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Controllers.ValidationData;
using SiteTask.Model.OriginalModel;

namespace SiteTask.Controllers.Data.Save;

public interface ISaveDataUserController
{
    public Task<IActionResult> SaveData(SecretData secretData);
}

[Route("/api/[controller]")]
[ApiController]
public class SaveDataUserController : ControllerBase, ISaveDataUserController
{

    private string _connect;

    private ILogger<SaveDataUserController> _logger;
    private IValidationController<string> _validationUser;

    private MySqlConnection _mySqlConnection;
    private MySqlCommand _mySqlCommand;

    public SaveDataUserController(IConfiguration configuration, ILogger<SaveDataUserController> logger)
    {
        _logger = logger;
        _connect = configuration.GetConnectionString("DefaultConnection");
        _validationUser = new ValidationController<string>(configuration);
    }
    
    [HttpPost("saveData")]
    public async Task<IActionResult> SaveData(SecretData secretData)
    {
        var isEmptyUser = _validationUser.SearchData(
            secretData.Login, "Users", "login");

        if (!isEmptyUser.Result)
            return NoContent();

        const string command = "INSERT INTO DataUsers(" +
                               "login, ip, macaddress, oc, pc, browser) " +
                               "VALUES(@Login, @Ip, @MacAddress, " +
                               "@Oc, @Pc, @Browser)";

        _mySqlCommand = new MySqlCommand(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("@Login", MySqlDbType.VarChar).Value = secretData.Login;
        _mySqlCommand.Parameters.Add("@Ip", MySqlDbType.VarChar).Value = secretData.Login;
        _mySqlCommand.Parameters.Add("@MacAddress", MySqlDbType.VarChar).Value = secretData.Login;
        _mySqlCommand.Parameters.Add("@Oc", MySqlDbType.VarChar).Value = secretData.Login;
        _mySqlCommand.Parameters.Add("@Pc", MySqlDbType.VarChar).Value = secretData.Login;
        _mySqlCommand.Parameters.Add("@Browser", MySqlDbType.VarChar).Value = secretData.Login;

        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();
            
        return Ok();
    }
}