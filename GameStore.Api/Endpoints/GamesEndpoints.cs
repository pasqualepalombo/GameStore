namespace GameStore.Api.Endpoints;

using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    //il tipo del this è il tipo di app di program.cs
    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // GET endpoints
        group.MapGet("/", async (GameStoreContext dbContext) =>
        await dbContext.Games
            .Include(game => game.Genre)
            .Select(game => new GameSummaryDto(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
                ))
            .AsNoTracking()
            .ToListAsync()
            );

        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) => {

            //var game = games.Find(game => game.Id == id);
            var game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(
                new GameDetailsDto (
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
                ));

        }).WithName(GetGameEndpointName);;

        // POST endpoints
        group.MapPost("/", async (GameCreateDto newGame, GameStoreContext dbContext) =>
        {

            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            GameDetailsDto gameDto = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );

            //standardard REST e HTTP
            //si imposta lo status code, si aggiunge l'header e si invia il corpo della risposta.
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = gameDto.Id}, gameDto);
        });


        // PUT endpoints
        group.MapPut("/{id}", async (int id, GameUpdateDto updatedGame, GameStoreContext dbContext) => {
                var existingGame = await dbContext.Games.FindAsync(id);

                if (existingGame is null) return Results.NotFound();

                existingGame.Name = updatedGame.Name;
                existingGame.GenreId = updatedGame.GenreId;
                existingGame.Price = updatedGame.Price;
                existingGame.ReleaseDate = updatedGame.ReleaseDate;

                await dbContext.SaveChangesAsync();
                // ritorno un 204 No Content (vedi se era meglio 200 Ok o 201 Created)
                return Results.NoContent();
            });


        // DELETE endpoints
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games
                .Where(game => game.Id == id)
                .ExecuteDeleteAsync();

            return Results.NoContent();
        });

    }

}