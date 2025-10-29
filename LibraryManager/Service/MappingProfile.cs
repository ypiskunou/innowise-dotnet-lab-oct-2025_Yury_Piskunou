using AutoMapper;
using Entities;
using Shared.DataTransferObjects;

namespace Service;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<Book, BookDto>();

        CreateMap<AuthorForCreationDto, Author>();
        CreateMap<AuthorForUpdateDto, Author>();
        CreateMap<BookForCreationDto, Book>();
        CreateMap<BookForUpdateDto, Book>();

        CreateMap<Author, AuthorWithBookCountDto>()
            .ForMember(dest => dest.BookCount,
                opt => opt.MapFrom(src => src.Books.Count()));
    }
}