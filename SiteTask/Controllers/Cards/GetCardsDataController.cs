using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Model;

namespace SiteTask.Controllers.Cards;

[Route("api/[controller]")]
[ApiController]
public class GetCardsDataController : ControllerBase
{
    private ILogger<GetCardsDataController> _logger;
    private string _connect;

    public GetCardsDataController(ILogger<GetCardsDataController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connect = configuration.GetValue<string>("ConnectionStrings");
    }

    [HttpGet("get_cards")]
    public async Task<IActionResult> CardsGet()
    {
        var mySqlConnect = new MySqlConnection(_connect);
        const string commandName = "SELECT name FROM CardDataShop";
        const string commandImg = "SELECT img FROM CardDataShop";
        const string commandDescription = "SELECT description FROM CardDataShop";

        await mySqlConnect.OpenAsync();

        var mySqlCommandName = new MySqlCommand(commandName, mySqlConnect);
        var mySqlCommandImg = new MySqlCommand(commandImg, mySqlConnect);
        var mySqlCommandDescription = new MySqlCommand(commandDescription, mySqlConnect);
        var mySqlAdapterName = new MySqlDataAdapter(mySqlCommandName);
        var mySqlAdapterImg = new MySqlDataAdapter(mySqlCommandImg);
        var mySqlAdapterDescription = new MySqlDataAdapter(mySqlCommandDescription);

        var dataSetName = new DataSet();
        var dataSetImg = new DataSet();
        var dataSetDescription = new DataSet();

        await mySqlAdapterName.FillAsync(dataSetName);
        await mySqlAdapterImg.FillAsync(dataSetImg);
        await mySqlAdapterDescription.FillAsync(dataSetDescription);
        var listCards = new List<Model.Cards>();
        foreach (DataRow dataRowName in dataSetName.Tables[0].Rows)
        {
            for (var i = 0; i < dataSetName.Tables[0].Columns.Count; i++)
            {
                foreach (DataRow dataRowImg in dataSetImg.Tables[0].Rows)
                {
                    for (var j = 0; j < dataSetImg.Tables[0].Columns.Count; j++)
                    {
                        foreach (DataRow dataRowDescription in dataSetDescription.Tables[0].Rows)
                        {
                            for (var g = 0; g < dataSetDescription.Tables[0].Columns.Count; g++)
                            {
                                var cards = new Model.Cards(dataRowName[i].ToString(), dataRowImg[j].ToString(),
                                    dataRowDescription[g].ToString());
                                listCards.Add(cards);
                            }
                        }
                    }
                }
            }
        }

        await mySqlConnect.CloseAsync();
        return Ok(listCards);
    }

    [HttpGet("get_cards{id:int}")]
    public async Task<IActionResult> CardsGetId(int id)
    {
        var mySqlConnect = new MySqlConnection(_connect);
        const string commandName = "SELECT name FROM CardDataShop WHERE id = @Id ";
        const string commandImg = "SELECT img FROM CardDataShop WHERE id = @Id";
        const string commandDescription = "SELECT description FROM CardDataShop WHERE id = @Id";

        await mySqlConnect.OpenAsync();

        var mySqlCommandName = new MySqlCommand(commandName, mySqlConnect);
        var mySqlCommandImg = new MySqlCommand(commandImg, mySqlConnect);
        var mySqlCommandDescription = new MySqlCommand(commandDescription, mySqlConnect);
        mySqlCommandName.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        mySqlCommandImg.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        mySqlCommandDescription.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        var mySqlAdapterName = new MySqlDataAdapter(mySqlCommandName);
        var mySqlAdapterImg = new MySqlDataAdapter(mySqlCommandImg);
        var mySqlAdapterDescription = new MySqlDataAdapter(mySqlCommandDescription);

        var dataSetName = new DataSet();
        var dataSetImg = new DataSet();
        var dataSetDescription = new DataSet();

        await mySqlAdapterName.FillAsync(dataSetName);
        await mySqlAdapterImg.FillAsync(dataSetImg);
        await mySqlAdapterDescription.FillAsync(dataSetDescription);
        var listCards = new List<Model.Cards>();
        foreach (DataRow dataRowName in dataSetName.Tables[0].Rows)
        {
            for (var i = 0; i < dataSetName.Tables[0].Columns.Count; i++)
            {
                foreach (DataRow dataRowImg in dataSetImg.Tables[0].Rows)
                {
                    for (var j = 0; j < dataSetImg.Tables[0].Columns.Count; j++)
                    {
                        foreach (DataRow dataRowDescription in dataSetDescription.Tables[0].Rows)
                        {
                            for (var g = 0; g < dataSetDescription.Tables[0].Columns.Count; g++)
                            {
                                var cards = new Model.Cards(dataRowName[i].ToString(), dataRowImg[j].ToString(),
                                    dataRowDescription[g].ToString());
                                listCards.Add(cards);
                            }
                        }
                    }
                }
            }
        }

        await mySqlConnect.CloseAsync();
        return Ok(listCards);
    }
}