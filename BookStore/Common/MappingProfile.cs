using AutoMapper;
using BookStore.Aplication.GenreOperations.Queries.GetGenres;
using BookStore.BookOperations.GetBooks;
using BookStore.Entities;
using BookStore.GetBookDetail;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;

namespace BookStore.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src =>src.Genre.Name));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)); //book=source, BooksViewModel=target
            CreateMap<Genre, GenresViewModel>();
        
        }
    }
}
