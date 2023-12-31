using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Model;
using SiteTask.Model.GetModel;
using SiteTask.SrtucturData;

namespace SiteTask.Controllers.Admin.Get;

public interface IGetAdminController
{
    public Task<IActionResult> GetAdminId(int id);
    public Task<IActionResult> GetAdminAll();
}

[Route("/api/[controller]")]
[ApiController]
public class GetAdminController : ControllerBase,IGetAdminController
{
    private ILogger<GetAdminController> _logger;
    private string _connect;
    
    private MySqlConnection _mySqlConnection;
    private MySqlCommand _mySqlCommand;
    private DbDataReader _dataReader;
    
    private Heap<AdminGet> _heap;
    private List<AdminGet> _gets;

    public GetAdminController(ILogger<GetAdminController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _heap = new Heap<AdminGet>();
        _gets = new List<AdminGet>();
        _connect = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpGet("getAdmin/{id:int}")]
    public async Task<IActionResult> GetAdminId(int id)
    {
        const string command = "SELECT * FROM Admin " +
                               "WHERE id = @ID";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("@ID", MySqlDbType.Int64).Value = id;

        _dataReader = await _mySqlCommand.ExecuteReaderAsync();

        if (_dataReader.HasRows)
        {
            while (await _dataReader.ReadAsync())
            {
                var idUser = _dataReader.GetValue(1);
                var rang = _dataReader.GetValue(2);

                var getAdmin = new AdminGet(id,idUser, rang);
                
                return Ok(getAdmin);
            }
        }
        
        await _mySqlConnection.CloseAsync();
        
        return NoContent();
    }

    [HttpGet("getAdmin")]
    public async Task<IActionResult> GetAdminAll()
    {
        const string command = "SELECT * FROM Admin";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);

        _dataReader = await _mySqlCommand.ExecuteReaderAsync();

        if (_dataReader.HasRows)
        {
            while (await _dataReader.ReadAsync())
            {
                var id = _dataReader.GetValue(0);
                var idUser = _dataReader.GetValue(1);
                var rang = _dataReader.GetValue(2);

                var getAdmin = new AdminGet(id,idUser, rang);
                _heap.Put(getAdmin);
            }

            while (_heap.Count > 0)
            {
                _gets.Add(_heap.GetMax());
            }
            
            
            return Ok(_gets);
        }
        
        await _mySqlConnection.CloseAsync();
        
        return NoContent();
    }
}