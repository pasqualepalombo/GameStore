//Sezione di application configuration
using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
// aggiunge la validazione messa nei dto
builder.Services.AddValidation();
// aggiunge il dbContext con il connectionString
var connectionString = "Data Source=GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();

//Sezione HTTP Request Pipeline
app.MapGamesEndpoints();

app.MigrateDb();

app.Run();
