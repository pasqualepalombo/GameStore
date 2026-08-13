namespace GameStore.Api.Dtos;

public record GameUpdateDto (
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);