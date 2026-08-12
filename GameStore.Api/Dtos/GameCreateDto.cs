namespace GameStore.Api.Dtos;

public record class GameCreateDto (
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);