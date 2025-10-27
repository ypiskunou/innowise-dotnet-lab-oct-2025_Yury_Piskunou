using Entities;

namespace Repository;

internal static class InMemoryDataStorage
{
    public static List<Author?> Authors { get; } = new List<Author?>();
    public static List<Book?> Books { get; } = new List<Book?>();
}