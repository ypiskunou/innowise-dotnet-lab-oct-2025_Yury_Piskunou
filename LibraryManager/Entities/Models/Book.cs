namespace Entities;

public class Book
{
    public Guid Id {get; set;}
    public string Title {get; set;}
    public int PublishedYear {get; set;}
    public Guid AuthorId {get; set;}
}