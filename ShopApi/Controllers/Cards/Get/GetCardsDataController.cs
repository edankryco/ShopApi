using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Model;
using SiteTask.Model.GetModel;
using SiteTask.SrtucturData;

namespace SiteTask.Controllers.Cards;

public interface IGetCardsDataController
{
    public Task<IActionResult> CardsGet();
    public Task<IActionResult> CardsGetId(int id);
}

[Route("api/[controller]")]
[ApiController]
public class GetCardsDataController : ControllerBase, IGetCardsDataController
{
    private ILogger<GetCardsDataController> _logger;
    private string _connect;
    
    private MySqlCommand _mySqlCommand;
    private MySqlConnection _mySqlConnect;
    private DbDataReader _dbDataReader;
    
    private IHeap<CardsGet> _heap;
    private List<CardsGet> _gets;

    public GetCardsDataController(ILogger<GetCardsDataController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _heap = new Heap<CardsGet>();
        _gets = new List<CardsGet>();
        _connect = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpGet("getCards/{id:int}")]
    public async Task<IActionResult> CardsGetId(int id)
    {
        const string command = "SELECT * FROM CardDataShop" +
                               "WHERE id = @ID";

        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        _mySqlCommand.Parameters.Add("@ID", MySqlDbType.UInt64).Value = id;

        _dbDataReader = await _mySqlCommand.ExecuteReaderAsync();
        if (_dbDataReader.HasRows)
        {
            while (await _dbDataReader.ReadAsync())
            {
                var name = _dbDataReader.GetValue(1);
                var img = _dbDataReader.GetValue(2);
                var idUser = _dbDataReader.GetValue(3);
                var description = _dbDataReader.GetValue(4);

                var cardsGet = new CardsGet(id, name, img, idUser, description);

                return Ok(cardsGet);
            }
        }
        await _mySqlConnect.CloseAsync();

        return NoContent();
    }

    [HttpGet("getCards")]
    public async Task<IActionResult> CardsGet()
    {
        const string command = "SELECT * FROM CardDataShop";

        _mySqlConnect = new MySqlConnection(_connect);
        await _mySqlConnect.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnect);
        _dbDataReader = await _mySqlCommand.ExecuteReaderAsync();

        if (_dbDataReader.HasRows)
        {
            while (await _dbDataReader.ReadAsync())
            {
                var id = _dbDataReader.GetValue(0);
                var name = _dbDataReader.GetValue(1);
                var img = _dbDataReader.GetValue(2);
                var idUser = _dbDataReader.GetValue(3);
                var description = _dbDataReader.GetValue(4);

                var cardsGet = new CardsGet(id, name, img, idUser, description);

                _heap.Put(cardsGet);
            }

            while (_heap.Size() > 0)
            {
                _gets.Add(_heap.GetMax());
            }

            return Ok(_gets);
        }

        await _mySqlConnect.CloseAsync();

        return NoContent();
    }
}