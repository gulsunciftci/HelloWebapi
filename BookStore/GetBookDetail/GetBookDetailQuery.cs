using BookStore.Common;
using BookStore.DBOperations;
using System;
using System.Linq;

namespace BookStore.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
            
            if(book is null)
            {
                throw new InvalidOperationException("kitap bulunamadı");
            }
            BookDetailViewModel vb = new BookDetailViewModel();
            vb.Title = book.Title;
            vb.PageCount = book.PageCount;
            vb.PublishDate = book.PublishDate.ToString("dd/MM/yyyy");
            vb.Genre = ((GenreEnum)book.GenreId).ToString();
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
