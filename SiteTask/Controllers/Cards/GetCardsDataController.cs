using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Model;

namespace SiteTask.Controllers.Cards;

[Route("api/[controller]")]
[ApiController]
public class GetCardsDataController : ControllerBase
{
    private string Connect = "Server=localhost;port=60341;Database=CardDataShop;Uid=root;pwd=root;charset=utf8";

    [HttpGet("get_cards")]
    public async Task<IActionResult> CardsGet()
    {
        var mySqlConnect = new MySqlConnection(Connect);
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

        var name = "";
        foreach (DataRow dataRowName in dataSetName.Tables[0].Rows)
        {
            for (var i = 0; i < dataSetName.Tables[0].Columns.Count; i++)
            {
                name = dataRowName[i].ToString();
            }
        }

        var cards = new Model.Cards(name, "", "");
        listCards.Add(cards);
        await mySqlConnect.CloseAsync();
        return Ok(listCards);
    }

    [HttpGet("get_cards{id:int}")]
    public async Task<IActionResult> CardsGetId(int id)
    {
        var mySqlConnect = new MySqlConnection(Connect);
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
        var mySqlAdapterImg = new MySqlDataAdapter(mySqlCommandName);
        var mySqlAdapterDescription = new MySqlDataAdapter(mySqlCommandName);
        
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
                    for (var j = 0; i < dataSetName.Tables[0].Columns.Count; i++)
                    {
                        foreach (DataRow dataRowDescription in dataSetDescription.Tables[0].Rows)
                        {
                            for (var g = 0; i < dataSetName.Tables[0].Columns.Count; i++)
                            {
                                var cards = new Model.Cards(dataRowName[i].ToString(), dataRowImg[i].ToString(), dataRowDescription[i].ToString());
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