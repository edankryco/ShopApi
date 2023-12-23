using MySql.Data.MySqlClient;

namespace SiteTask.CreateTable;

public class CreateTable
{
    private MySqlCommand _mySqlCommand;
    private MySqlConnection _mySqlConnection;
    private string _conenct;

    public CreateTable(MySqlCommand mySqlCommand, MySqlConnection mySqlConnection, string conenct)
    {
        _mySqlCommand = mySqlCommand;
        _mySqlConnection = mySqlConnection;
        _conenct = conenct;
    }

    public async Task StartSearch()
    {
        await CreateTableUsers();
        await CreateTableCards();
        await PurchaseHistoryTable();
    }

    private async Task PurchaseHistoryTable()
    {
        const string command = "CREATE TABLE IF NOT EXISTS History(" +
                               "id INT AUTO_INCREMENT PRIMARY KEY NOT NULL," +
                               "iduser INT, " +
                               "buy INT, " +
                               "cardsname VARCHAR(255) " +
                               "FOREIGN KEY iduser Users(name), " +
                               "FOREIGN KEY buy Cards(id))";

        _mySqlConnection = new MySqlConnection(_conenct);
        await _mySqlConnection.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();
    }

    private async Task CreateTableUsers()
    {
        const string command = "CREATE TABLE IF NOT EXISTS Users(" +
                               "id INT AUTO_INCREMENT PRIMARY KEY NOT NULL," +
                               "name VARCHAR(255)," +
                               "age INT(3), " +
                               "password INT,  " +
                               "email VARCHAR(255), " +
                               "balanc INT)";

        _mySqlConnection = new MySqlConnection(_conenct);
        await _mySqlConnection.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();
    }

    private async Task CreateTableCards()
    {
        const string command = "CREATE TABLE IF NOT EXISTS Cards(" +
                               "id INT AUTO_INCREMENT PRIMARY KEY NOT NULL," +
                               " namecards VARCHAR(255), " +
                               "img BIT, " +
                               "nameuser VARCHAR(255))";

        _mySqlConnection = new MySqlConnection(_conenct);
        await _mySqlConnection.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();
    }
}