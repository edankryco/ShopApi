using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserGetController : ControllerBase
{
    string connect = "Server=localhost;port=49302;Database=Click;Uid=root;pwd=root;charset=utf8";
    [HttpGet("get_userBase")]
    public IActionResult GetUser()
    {
        List<string> listUser = new List<string>();
        var mysqlConnect = new MySqlConnection(connect);
        mysqlConnect.Open();
        var command = "SELECT * FROM Click";
        var mysqlCommand = new MySqlCommand(command, mysqlConnect);
        var mySlAdapter = new MySqlDataAdapter(mysqlCommand);
        DataSet dataSet = new DataSet();
        mySlAdapter.Fill(dataSet);
        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
        {
            for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
            {
                listUser.Add(dataRow[i].ToString());
            }
        }
        if (listUser == null)
        {
            return NoContent();
        }
        mysqlConnect.Close();
        return Ok(listUser);
    }
 
    [HttpGet("get_userBase{id}")]
    public IActionResult GetUserId(int id)
    {
        List<string> listUser = new List<string>();
        var mysqlConnect = new MySqlConnection(connect);
        mysqlConnect.Open();
        var command = "SELECT * FROM Click WHERE id = @Id";
        var mysqlCommand = new MySqlCommand(command, mysqlConnect);
        mysqlCommand.Parameters.Add("@Id", MySqlDbType.Text).Value = id;
        var mySlAdapter = new MySqlDataAdapter(mysqlCommand);
        DataSet dataSet = new DataSet();
        mySlAdapter.Fill(dataSet);
        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
        {
            for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
            {
                listUser.Add(dataRow[i].ToString());
            }
        }
        mysqlConnect.Close();
        return Ok(listUser);
    }
}