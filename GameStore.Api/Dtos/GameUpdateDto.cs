using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record GameUpdateDto (
    [Required][StringLength(50)] string Name,
    [StringLength(30)] string Genre,
    [Required][Range(0,80)] decimal Price,
    [Required] DateOnly ReleaseDate
);