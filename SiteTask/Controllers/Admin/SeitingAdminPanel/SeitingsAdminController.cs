using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Model;

namespace SiteTask.Controllers.Admin;

public interface ISeitingsAdminController
{
    public Task<IActionResult> CreateAdmin(Model.Admin admin);
    public Task<IActionResult> DeletedAdmin(int id);
    public Task<IActionResult> UpRang(int id, int newRang);
}

[Route("/api/[controller]")]
[ApiController]
public class SeitingsAdminController : ControllerBase, ISeitingsAdminController
{
    private MySqlCommand _mySqlCommand;
    private MySqlConnection _mySqlConnect;
    private ILogger<SeitingsAdminController> _logger;
    private string _connect;

    public SeitingsAdminController(IConfiguration configuration, ILogger<SeitingsAdminController> logger)
    {
        _logger = logger;
        _connect = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpPost("createadmin")]
    public async Task<IActionResult> CreateAdmin(Model.Admin admin)
    {
        const string command = "INSERT INTO Admin(login, rang) " +
                               "VALUES(@Login, @Rang)";

        _mySqlConnect = new MySqlConnection(_connect);
        
        await _mySqlConnect.OpenAsync();
        
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        
        _mySqlCommand.Parameters.Add("@Login", MySqlDbType.VarChar).Value = admin.Login;
        _mySqlCommand.Parameters.Add("@Rang", MySqlDbType.Int64).Value = admin.Rang;
        
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnect.CloseAsync();

        return NoContent();
    }

    [HttpDelete("deletedadmin")]
    public async Task<IActionResult> DeletedAdmin(int id)
    {
        const string command = "DELETE FROM Admin WHERE " +
                               "iduser = @ID";

        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();
        
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        
        _mySqlCommand.Parameters.Add("@ID", MySqlDbType.Int64).Value = id;
        
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnect.CloseAsync();
        
        return NoContent();
    }

    [HttpPut("uprang")]
    public async Task<IActionResult> UpRang(int id, int newRang)
    {
        const string command = "UPDATE Admin SET " +
                               "rang = @Rang WHERE " +
                               "iduser = @ID";
        
        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();
        
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        
        _mySqlCommand.Parameters.Add("@ID", MySqlDbType.Int64).Value = id;
        _mySqlCommand.Parameters.Add("@Rang", MySqlDbType.Int64).Value = newRang;
        
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnect.CloseAsync();
        
        return NoContent();
    }
}