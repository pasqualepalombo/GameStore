//Sezione di application configuration
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Sezione HTTP Request Pipeline
app.MapGamesEndpoints();

app.Run();
