namespace Entities;

public class Author
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public ICollection<Book> Books { get; set; }
}