namespace Entities;

public class Book
{
    Guid Id {get; set;}
    string Title {get; set;}
    DateTime PublishedYear {get; set;}
    Guid AuthorId {get; set;}
}