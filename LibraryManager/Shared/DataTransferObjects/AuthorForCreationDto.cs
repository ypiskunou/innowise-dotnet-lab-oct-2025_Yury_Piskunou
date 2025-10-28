using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record AuthorForCreationDto(
    [Required(ErrorMessage = "Author name is required")]
    [MinLength(2, ErrorMessage = "Author name must be at least 2 characters")]
    [MaxLength(50, ErrorMessage = "Author name cannot be more than 50 characters")]
    string Name, 
    
    [Required(ErrorMessage = "Author birth date is required")]
    DateTime DateOfBirth
    );