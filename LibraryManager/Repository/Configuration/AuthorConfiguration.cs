using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class AuthorConfiguration: IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasData(
            new Author 
            { 
                Id = new Guid("3310f8f9-f4bd-4a88-81fc-d50fdc2bc7dd"), 
                Name = "Ray Bradbury", 
                DateOfBirth = new DateTime(1920, 8, 22) 
            },
            new Author 
            { 
                Id = new Guid("d6b0b215-9108-493c-985c-1b541a2c92ef"), 
                Name = "Isaac Asimov", 
                DateOfBirth = new DateTime(1920, 1, 2) 
            },
            new Author 
            { 
                Id = new Guid("f16c5009-1386-4be4-a4cc-8944fe7e92c1"), 
                Name = "Terry Pratchett", 
                DateOfBirth = new DateTime(1948, 4, 28) 
            }
        );
        
        builder.HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}