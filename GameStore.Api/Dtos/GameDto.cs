namespace GameStore.Api.Dtos;

//Il Data Transfer Object è il formato di comune accordo con cui client e server
//trasferiscono l'oggetto e le sue caratteristiche
public record GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);