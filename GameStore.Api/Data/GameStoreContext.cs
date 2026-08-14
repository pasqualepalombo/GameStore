using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    //mapping properties between objects and database tables
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();
}