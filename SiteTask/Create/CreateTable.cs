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
        await AdminTable();
        await PurchaseHistoryTable();
    }

    private async Task AdminTable()
    {
        const string command = "CREATE TABLE IF NOT EXISTS Admin(" +
                               "id INT PRIMARY KEY AUTO_INCREMENT NOT NULL, " +
                               "Name VARCHAR(255), " +
                               "idadmin INT, " +
                               "rang INT," +
                               "FOREIGN KEY (idadmin) REFERENCES Users (id))";
        
        _mySqlConnection = new MySqlConnection(_conenct);
        await _mySqlConnection.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();
    }

    private async Task PurchaseHistoryTable()
    {
        const string command = "CREATE TABLE IF NOT EXISTS History(" +
                               "id INT PRIMARY KEY AUTO_INCREMENT NOT NULL," +
                               "iduser INT, " +
                               "buy INT, " +
                               "cardsname VARCHAR(255), " +
                               "FOREIGN KEY (iduser) REFERENCES Users(id), " +
                               "FOREIGN KEY (buy) REFERENCES Cards(id))";

        _mySqlConnection = new MySqlConnection(_conenct);
        await _mySqlConnection.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();
    }

    private async Task CreateTableUsers()
    {
        const string command = "CREATE TABLE IF NOT EXISTS Users(" +
                               "id INT PRIMARY KEY AUTO_INCREMENT NOT NULL," +
                               "name VARCHAR(255)," +
                               "age INT(3)," +
                               "email VARCHAR(255)," +
                               "password INT, " +
                               "repassword INT," +
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
                               "id INT PRIMARY KEY AUTO_INCREMENT NOT NULL," +
                               "namecards VARCHAR(255), " +
                               "img BIT, " +
                               "nameuser VARCHAR(255))";

        _mySqlConnection = new MySqlConnection(_conenct);
        await _mySqlConnection.OpenAsync();
        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();
    }
}