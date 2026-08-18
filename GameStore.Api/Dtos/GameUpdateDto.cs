using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record GameUpdateDto (
    [Required][StringLength(50)] string Name,
    [Range(1,50)] int GenreId,
    [Required][Range(0,999)] decimal Price,
    [Required] DateOnly ReleaseDate
);