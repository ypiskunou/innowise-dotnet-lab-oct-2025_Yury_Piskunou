using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Book
{
    public Guid Id {get; set;}
    public string Title {get; set;}
    public int PublishedYear {get; set;}
    public Guid AuthorId {get; set;}
    public Author Author {get; set;}
}