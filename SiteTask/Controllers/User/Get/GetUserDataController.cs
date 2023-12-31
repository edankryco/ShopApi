using System.Data;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Model;
using SiteTask.Model.GetModel;
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
    private ILogger<UserGetController> _logger;
    private string _connect;
    
    private MySqlCommand _mySqlCommand;
    private MySqlConnection _mySqlConnect;
    private DbDataReader _dbDataReader;
    
    private Heap<UsersGet> _heap;
    private List<UsersGet> _gets;

    public UserGetController(ILogger<UserGetController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _heap = new Heap<UsersGet>();
        _gets = new List<UsersGet>();
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
        _dbDataReader = await _mySqlCommand.ExecuteReaderAsync();
        if (_dbDataReader.HasRows)
        {
            while (await _dbDataReader.ReadAsync())
            {
                var login = _dbDataReader.GetValue(1);
                var name = _dbDataReader.GetValue(2);
                var age = _dbDataReader.GetValue(3);
                var email = _dbDataReader.GetValue(4);
                var password = _dbDataReader.GetValue(5);
                var repeatPassword = _dbDataReader.GetValue(6);
                var balanc = _dbDataReader.GetValue(7);
                
                var userGet = new UsersGet(login, name, age, email, password, repeatPassword, balanc);
                
                return Ok(userGet);
            }
        }

        await _mySqlConnect.CloseAsync();

        return NoContent();
    }

    [HttpGet("getUser")]
    public async Task<IActionResult> GetUsers()
    {
        const string command = "SELECT * FROM Users";
        
        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        _dbDataReader = await _mySqlCommand.ExecuteReaderAsync();
        if (_dbDataReader.HasRows)
        {
            while (await _dbDataReader.ReadAsync())
            {
                var login = _dbDataReader.GetValue(1);
                var name = _dbDataReader.GetValue(2);
                var age = _dbDataReader.GetValue(3);
                var email = _dbDataReader.GetValue(4);
                var password = _dbDataReader.GetValue(5);
                var repeatPassword = _dbDataReader.GetValue(6);
                var balans = _dbDataReader.GetValue(7);
                
                var userGet = new UsersGet(login, name, age, email, password, repeatPassword, balans);
                _heap.Put(userGet);
            }

            while (_heap.Count > 0)
            {
                _gets.Add(_heap.GetMax());
            }
            
            return Ok(_gets);
        }

        await _mySqlConnect.CloseAsync();

        return NoContent();
    }
}