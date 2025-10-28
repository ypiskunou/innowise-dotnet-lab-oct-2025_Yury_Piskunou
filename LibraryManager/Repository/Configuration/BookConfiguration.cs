using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasData(
            new Book
            {
                Id = new Guid("4145bbe3-5e1e-4034-9d54-da63f56c02c3"), 
                Title = "Fahrenheit 451", 
                PublishedYear = 1953,
                AuthorId = new Guid("3310f8f9-f4bd-4a88-81fc-d50fdc2bc7dd")
            },
            new Book
            {
                Id = new Guid("560a5138-59b7-43be-aac4-03056c4a27d8"), 
                Title = "The Martian Chronicles",
                PublishedYear = 1950, 
                AuthorId = new Guid("3310f8f9-f4bd-4a88-81fc-d50fdc2bc7dd")
            },
            new Book
            {
                Id = new Guid("d6808a67-50c8-493f-b224-3ecc4cabab1b"), 
                Title = "Dandelion Wine", 
                PublishedYear = 1957,
                AuthorId = new Guid("3310f8f9-f4bd-4a88-81fc-d50fdc2bc7dd")
            },
            new Book
            {
                Id = new Guid("dfc4017b-733e-49b3-b915-0572a729ffa0"), 
                Title = "I, Robot", 
                PublishedYear = 1950,
                AuthorId = new Guid("d6b0b215-9108-493c-985c-1b541a2c92ef")
            },
            new Book
            {
                Id = new Guid("09820e43-229b-42c6-9f38-39f58bbefb58"), 
                Title = "Foundation", 
                PublishedYear = 1951,
                AuthorId = new Guid("d6b0b215-9108-493c-985c-1b541a2c92ef")
            },
            new Book
            {
                Id = new Guid("4e4a2cf1-6bb3-462c-b8cd-208e9161cb9a"), 
                Title = "The Colour of Magic",
                PublishedYear = 1983, 
                AuthorId = new Guid("f16c5009-1386-4be4-a4cc-8944fe7e92c1")
            },
            new Book
            {
                Id = new Guid("236187cc-6c1c-48e5-8fc4-6df2976f31ca"), 
                Title = "Mort", 
                PublishedYear = 1987,
                AuthorId = new Guid("f16c5009-1386-4be4-a4cc-8944fe7e92c1")
            }
        );
    }
}