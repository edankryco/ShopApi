using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiteTask.Model;

namespace SiteTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserGetController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<UserGetController> _logger;

    public UserGetController(ILogger<UserGetController> logger)
    {
        _logger = logger;
    }

    string connect = "Server=localhost;port=51363;Database=Click;Uid=root;pwd=root;charset=utf8";

    [HttpGet("get_userBase")]
    public async Task<IActionResult> GetUser()
    {
        string name = "";
        var mysqlConnect = new MySqlConnection(connect);
        await mysqlConnect.OpenAsync();
        var command = "SELECT name FROM Click";
        var mysqlCommand = new MySqlCommand(command, mysqlConnect);
        var mySlAdapter = new MySqlDataAdapter(mysqlCommand);
        DataSet dataSet = new DataSet();
        await mySlAdapter.FillAsync(dataSet);
        var listName = new List<User>();
        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
        {
            for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
            {
                var user = new User(dataRow[i].ToString(), "wdf", "123", "123", 0);
                listName.Add(user);
            }
        }
       
        await mysqlConnect.CloseAsync();
        return Ok(listName);
    }

    [HttpGet("get_userBase/{id:int}")]
    public async Task<IActionResult> GetUserId(int id)
    {
        var mysqlConnect = new MySqlConnection(connect);
        await mysqlConnect.OpenAsync();
        const string command = "SELECT name FROM Click WHERE id = @Id";
        var mysqlCommand = new MySqlCommand(command, mysqlConnect);
        mysqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        var mySlAdapter = new MySqlDataAdapter(mysqlCommand);
        DataSet dataSet = new DataSet();
        await mySlAdapter.FillAsync(dataSet);
        var name = "";
        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
        {
            for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
            {
                name = dataRow[i].ToString();
            }
        }

        var listUser = new List<User>
        {
            new User(name, "eledyall", "123", "123", 0)
        };
        await mysqlConnect.CloseAsync();
        return Ok(listUser);
    }
}