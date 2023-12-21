using MySql.Data.MySqlClient;

namespace SiteTask.CreateTable;

public class CreateTable
{
    private IConfiguration _configuration = new ConfigurationManager();
    private static string _connect;
    
    private MySqlCommand _mySqlCommand = new();
    private MySqlConnection _mySqlConnection = new();
    
    public void StartSearch()
    {
        CreateTableUsers();
        CreateTableCards();
        PurchaseHistoryTable();
    }

    private void PurchaseHistoryTable()
    {
        
    }

    private void CreateTableUsers()
    {
        const string command = "CREATE TABLE Users(name VARCHAR(255) PRIMARY KEY," +
                               "age INT(3), " +
                               "password INT,  " +
                               "email VARCHAR(255), " +
                               "balanc INT)";
    }

    private void CreateTableCards()
    {
        const string command = "CREATE TABLE Cards(name VARCHAR(255), img BIT)";
    }
}