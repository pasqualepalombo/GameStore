namespace GameStore.Api.Endpoints;

using GameStore.Api.Dtos;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new (1, "The Blue Nowhere", "Adventure", 19.99m, new DateOnly(2027, 3, 5)),
        new(2, "Mi.mi.co", "Dungeon Crawler", 19.95m, new DateOnly(2028, 8, 11)),
        new(3, "Wildfire", "Adventure", 35.00m, new DateOnly(2028, 12, 24))
    ];

    //il tipo del this è il tipo di app di program.cs
    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // GET endpoints
        group.MapGet("/", () => games);
        group.MapGet("/{id}", (int id) => {

            var game = games.Find(game => game.Id == id);
            
            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndpointName);;

        // POST endpoints
        group.MapPost("/", (GameCreateDto newGame) =>
        {
            GameDto game = new(
                games.Count()+1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            //standardard REST e HTTP
            //si imposta lo status code, si aggiunge l'header e si invia il corpo della risposta.
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id}, game);
        });


        // PUT endpoints
        group.MapPut("/{id}", (int id, GameUpdateDto updatedGame) => {
                var index = games.FindIndex(game => game.Id == id);

                if (index == -1) return Results.NotFound();

                games[index] = new GameDto (
                    id,
                    updatedGame.Name,
                    updatedGame.Genre,
                    updatedGame.Price,
                    updatedGame.ReleaseDate
                );

                // ritorno un 204 No Content (vedi se era meglio 200 Ok o 201 Created)
                return Results.NoContent();
            });


        // DELETE endpoints
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

    }

}