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

    string connect = "Server=localhost;port=60341;Database=Click;Uid=root;pwd=root;charset=utf8";

    [HttpGet("userBase")]
    public async Task<IActionResult> GetUser()
    {
        var mysqlConnect = new MySqlConnection(connect);
        await mysqlConnect.OpenAsync();
        var commandName = "SELECT name FROM Click";
        var commandMail = "SELECT mail FROM Click";
        var commandPass = "SELECT pass FROM Click";
        var commandReplace_pass = "SELECT replace_pass FROM Click";
        var commandBalans = "SELECT balans FROM Click";

        var mysqlCommandName = new MySqlCommand(commandName, mysqlConnect);
        var mysqlCommandMail = new MySqlCommand(commandMail, mysqlConnect);
        var mysqlCommandPass = new MySqlCommand(commandPass, mysqlConnect);
        var mysqlCommandReplace_pass = new MySqlCommand(commandReplace_pass, mysqlConnect);
        var mysqlCommandBalans = new MySqlCommand(commandBalans, mysqlConnect);

        var mySlAdapterName = new MySqlDataAdapter(mysqlCommandName);
        var mySlAdapterMail = new MySqlDataAdapter(mysqlCommandMail);
        var mySlAdapterPass = new MySqlDataAdapter(mysqlCommandPass);
        var mySlAdapterReplace_pass = new MySqlDataAdapter(mysqlCommandReplace_pass);
        var mySlAdapterBalans = new MySqlDataAdapter(mysqlCommandBalans);

        var dataSetName = new DataSet();
        var dataSetMail = new DataSet();
        var dataSetPass = new DataSet();
        var dataSetReplace_pass = new DataSet();
        var dataSetBalans = new DataSet();

        await mySlAdapterName.FillAsync(dataSetName);
        await mySlAdapterMail.FillAsync(dataSetMail);
        await mySlAdapterPass.FillAsync(dataSetPass);
        await mySlAdapterReplace_pass.FillAsync(dataSetReplace_pass);
        await mySlAdapterBalans.FillAsync(dataSetBalans);

        var listUser = new List<User>();
        foreach (DataRow dataRowName in dataSetName.Tables[0].Rows)
        {
            for (int i = 0; i < dataSetName.Tables[0].Columns.Count; i++)
            {
                foreach (DataRow dataRowMail in dataSetMail.Tables[0].Rows)
                {
                    for (int j = 0; j < dataSetMail.Tables[0].Columns.Count; j++)
                    {
                        foreach (DataRow dataRowPass in dataSetPass.Tables[0].Rows)
                        {
                            for (int g = 0; g < dataSetPass.Tables[0].Columns.Count; g++)
                            {
                                foreach (DataRow dataRowReplace_pass in dataSetReplace_pass.Tables[0].Rows)
                                {
                                    for (int f = 0; f < dataSetReplace_pass.Tables[0].Columns.Count; f++)
                                    {
                                        foreach (DataRow dataRowBalans in dataSetBalans.Tables[0].Rows)
                                        {
                                            for (int d = 0; d < dataSetBalans.Tables[0].Columns.Count; d++)
                                            {
                                                var user = new User(dataRowName[i].ToString(),dataRowMail[j].ToString(),dataRowPass[g].ToString(),dataRowReplace_pass[f].ToString(),0);
                                                listUser.Add(user);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        await mysqlConnect.CloseAsync();
        return Ok(listUser);
    }

    [HttpGet("userBase/{id:int}")]
    public async Task<IActionResult> GetUserId(int id)
    {
        var mysqlConnect = new MySqlConnection(connect);
        await mysqlConnect.OpenAsync();

        const string command = "SELECT name FROM Click WHERE id = @Id";
        var mysqlCommand = new MySqlCommand(command, mysqlConnect);
        mysqlCommand.Parameters.Add("@Id", MySqlDbType.Int64).Value = id;
        var mySlAdapter = new MySqlDataAdapter(mysqlCommand);
        var dataSet = new DataSet();
        await mySlAdapter.FillAsync(dataSet);
        var listUser = new List<User>();
        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
        {
            for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
            {
                var user = new User(dataRow[i].ToString(), "eledyall", "123", "123", 0);
                listUser.Add(user);
            }
        }

        await mysqlConnect.CloseAsync();
        return Ok(listUser);
    }
}