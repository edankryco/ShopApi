using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SiteTask.Controllers.ValidationData;

public interface IValidationController<T>
{
    public Task<bool> SearchData(T item, string table, string name);
}

public class ValidationController<T> : IValidationController<T>
{
    private string _connect;

    private MySqlConnection _mySqlConnection;
    private MySqlCommand _mySqlCommand;

    public ValidationController(IConfiguration configuration)
    {
        _connect = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<bool> SearchData(T item, string table, string name)
    {
        var command = $"SELECT EXISTS(" +
                      $"SELECT {name} FROM {table} " +
                      $"WHERE {name} = @Name)";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.AddWithValue("@Name", item);

        var exist = await _mySqlCommand.ExecuteScalarAsync();
        var convertBoolean = Convert.ToBoolean(exist);
        
        await _mySqlConnection.CloseAsync();

        return convertBoolean;
    }
}