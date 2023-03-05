using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Include(x=>x.Genre).Where(book => book.Id == BookId).SingleOrDefault();
            
            if(book is null)
            {
                throw new InvalidOperationException("kitap bulunamadı");
            }

            BookDetailViewModel vb = _mapper.Map<BookDetailViewModel>(book);


            //BookDetailViewModel vb = new BookDetailViewModel();
            //vb.Title = book.Title;
            //vb.PageCount = book.PageCount;
            //vb.PublishDate = book.PublishDate.ToString("dd/MM/yyyy");
            //vb.Genre = ((GenreEnum)book.GenreId).ToString();
            return vb;
        }

    }
    public class BookDetailViewModel
    {
        public string Title { get; set; }

        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
