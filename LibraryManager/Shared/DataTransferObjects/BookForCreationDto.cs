using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record BookForCreationDto(
    [Required(ErrorMessage = "Title is required")]
    [MinLength(3, ErrorMessage = "Title must be at least 3 characters")]
    [MaxLength(50, ErrorMessage = "Title must be no more than 50 characters")]
    string Title, 
    
    int PublishedYear, 
    
    [Required(ErrorMessage = "Author is required")]
    Guid AuthorId
    );