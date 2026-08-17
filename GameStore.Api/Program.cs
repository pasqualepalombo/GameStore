using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
// aggiunge la validazione messa nei dto
builder.Services.AddValidation();
// aggiunge il dbContext con il connectionString
builder.AddGameStoreDb();

var app = builder.Build();

//Sezione HTTP Request Pipeline
app.MapGamesEndpoints();

app.MigrateDb();

app.Run();
