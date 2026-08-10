//Sezione di application configuration
using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//LISTA GIOCHI DA TOGLIERE POI
List<GameDto> games = [
    new (1, "The Blue Nowhere", "Adventure", 19.99m, new DateOnly(2027, 3, 5)),
    new(2, "Mi.mi.co", "Dungeon Crawler", 19.95m, new DateOnly(2028, 8, 11)),
    new(3, "Wildfire", "Adventure", 35.00m, new DateOnly(2028, 12, 24))
];


//Sezione HTTP Request Pipeline
// GET endpoints
app.MapGet("/games", () => games);
app.MapGet("/games/{id}", () => "Gioco preciso");



app.Run();
