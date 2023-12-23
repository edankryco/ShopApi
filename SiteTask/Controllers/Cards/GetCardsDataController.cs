using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Model;

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
    private MySqlCommand _mySqlCommand = new();
    private MySqlConnection _mySqlConnect = new();
    private ILogger<GetCardsDataController> _logger;
    private string _connect;

    public GetCardsDataController(ILogger<GetCardsDataController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connect = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpGet("get_cards")]
    public async Task<IActionResult> CardsGet()
    {
        return Ok();
    }

    [HttpGet("get_cards{id:int}")]
    public async Task<IActionResult> CardsGetId(int id)
    {
        return Ok();
    }
}