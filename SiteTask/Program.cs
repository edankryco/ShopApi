using MySql.Data.MySqlClient;
using SiteTask.CreateTable;

var mySqlConnection = new MySqlConnection();
var mySqlCommand = new MySqlCommand();
var connect = "Server=192.168.137.62;Database=DataUser;Uid=root;pwd=root;charset=utf8";

CreateTableConfig();


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
return;

void CreateTableConfig()
{
    var create = new CreateTable(mySqlCommand, mySqlConnection, connect);
    create.StartSearch();
}