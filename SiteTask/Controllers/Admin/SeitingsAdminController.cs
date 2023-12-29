using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Model;

namespace SiteTask.Controllers.Admin;

public interface ISeitingsAdminController
{
    public Task<IActionResult> CreateAdmin(Model.Admin admin);
    public Task<IActionResult> DeletedAdmin(int id);
    public Task<IActionResult> UpRang(int id);
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
        const string command = "INSERT INTO Admin(iduser, rang) " +
                               "VALUES(@IDADMIN, @RANG)";

        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        _mySqlCommand.Parameters.Add("@IDADMIN", MySqlDbType.Int64).Value = admin.IdAdmin;
        _mySqlCommand.Parameters.Add("@RANG", MySqlDbType.Int64).Value = admin.Rang;
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnect.CloseAsync();

        return NoContent();
    }

    [HttpPost("deletedadmin")]
    public async Task<IActionResult> DeletedAdmin(int id)
    {
        const string command = "DELETE FROM Admin WHERE " +
                               "idadmin = @ID";

        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        _mySqlCommand.Parameters.Add("@ID", MySqlDbType.Int64).Value = id;
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnect.CloseAsync();
        return NoContent();
    }

    [HttpPut("uprang")]
    public async Task<IActionResult> UpRang(int id)
    {
        const string command = "UPDATE Admin SET " +
                               "rang = @RANG WHERE " +
                               "idadmin = @ID";
        
        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        _mySqlCommand.Parameters.Add("@ID", MySqlDbType.Int64).Value = id;
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnect.CloseAsync();
        return NoContent();
    }
}