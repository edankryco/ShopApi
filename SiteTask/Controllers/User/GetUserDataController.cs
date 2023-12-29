using System.Data;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Model;
using SiteTask.SrtucturData;

namespace SiteTask.Controllers;

public interface IUserGetController
{
    public Task<IActionResult> GetUserId(int id);
    public Task<IActionResult> GetUsers();
}

[Route("api/[controller]")]
[ApiController]
public class UserGetController : ControllerBase, IUserGetController
{
    private MySqlCommand _mySqlCommand;
    private MySqlConnection _mySqlConnect;
    private ILogger<UserGetController> _logger;
    private string _connect;
    private Heap<UserGet> _heap = new();
    private List<UserGet> _gets = new();

    public UserGetController(ILogger<UserGetController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connect = configuration.GetConnectionString("DefaultConnection");
    }


    [HttpGet("getUser/{id:int}")]
    public async Task<IActionResult> GetUserId(int id)
    {
        const string command = "SELECT * FROM Users " +
                               "WHERE id = @ID";

        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        _mySqlCommand.Parameters.Add("ID", MySqlDbType.Int64).Value = id;
        var dbDataReader = await _mySqlCommand.ExecuteReaderAsync();
        if (dbDataReader.HasRows)
        {
            while (await dbDataReader.ReadAsync())
            {
                var login = dbDataReader.GetValue(1);
                var name = dbDataReader.GetValue(2);
                var age = dbDataReader.GetValue(3);
                var email = dbDataReader.GetValue(4);
                var password = dbDataReader.GetValue(5);
                var repeatPassword = dbDataReader.GetValue(6);
                var balanc = dbDataReader.GetValue(7);
                
                var userGet = new UserGet(login, name, age, email, password, repeatPassword, balanc);
                
                return Ok(userGet);
            }
        }

        await _mySqlConnect.CloseAsync();

        return NotFound();
    }

    [HttpGet("getUser")]
    public async Task<IActionResult> GetUsers()
    {
        const string command = "SELECT * FROM Users";
        
        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        var dbDataReader = await _mySqlCommand.ExecuteReaderAsync();
        if (dbDataReader.HasRows)
        {
            while (await dbDataReader.ReadAsync())
            {
                var login = dbDataReader.GetValue(1);
                var name = dbDataReader.GetValue(2);
                var age = dbDataReader.GetValue(3);
                var email = dbDataReader.GetValue(4);
                var password = dbDataReader.GetValue(5);
                var repeatPassword = dbDataReader.GetValue(6);
                var balans = dbDataReader.GetValue(7);
                
                var userGet = new UserGet(login, name, age, email, password, repeatPassword, balans);
                _heap.Put(userGet);
            }

            while (_heap.Count > 0)
            {
                _gets.Add(_heap.GetMax());
            }
            
            return Ok(_gets);
        }

        await _mySqlConnect.CloseAsync();

        return NotFound();
    }
}