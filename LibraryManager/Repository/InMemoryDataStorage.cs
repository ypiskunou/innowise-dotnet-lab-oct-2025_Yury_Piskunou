using System.Text.Json;
using System.Text.Json.Nodes;
using Entities;

namespace Repository;

internal static class InMemoryDataStorage
{
    public static List<Author?> Authors { get; }
    public static List<Book?> Books { get; } 

    static InMemoryDataStorage()
    {
        var basePath = AppContext.BaseDirectory;
        var authorsFilePath = Path.Combine(basePath, "Data/authors.json");
        
        var authorsText = File.ReadAllText(authorsFilePath);
        Authors = JsonSerializer
            .Deserialize<List<Author>>(authorsText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        var booksFilePath = Path.Combine(basePath, "Data/books.json");
        var booksText = File.ReadAllText(booksFilePath);
        Books = JsonSerializer
            .Deserialize<List<Book>>(booksText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}