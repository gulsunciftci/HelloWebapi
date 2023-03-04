using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper) //Constructor
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.Include(x=>x.Genre).OrderBy(x => x.Id).ToList<Book>();

            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);



            //List < BooksViewModel > vm = new List<BooksViewModel>();
            //foreach (var book in bookList)
            //{
            //    vm.Add(new BooksViewModel
            //    {

            //        Title = book.Title,
            //        Genre = ((GenreEnum)book.GenreId).ToString(),
            //        PublishDate=book.PublishDate.Date.ToString("dd/MM/yyy"),
            //        PageCount=book.PageCount



            //    }) ;


            //}




            return vm;
        }

    }
    public class BooksViewModel{


        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }

       
    }
}
