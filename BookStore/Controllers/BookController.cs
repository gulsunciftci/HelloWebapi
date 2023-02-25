using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]

    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>()
       {
           new Book
           {
               Id=1,
               Title="Lean Startup",
               GenreId=1,
               PageCount=200,
               PublishDate=new System.DateTime(2001,06,12)
           },
           new Book
           {
               Id=2,
               Title="Herland",
               GenreId=2,
               PageCount=250,
               PublishDate=new System.DateTime(2010,05,23)
           },
           new Book
           {
               Id=3,
               Title="Dune",
               GenreId=2,
               PageCount=540,
               PublishDate=new System.DateTime(2001,12,21)
           }
       };

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;


        }

        [HttpGet("{id}")] //roottan alıyoruz.Daha doğru olan yaklaşım budur yani roottan almaktır
        public Book GetById(int id)
        {
            var book = BookList.Where(book => book.Id == id).SingleOrDefault();
            return book;


        }
        //[HttpGet] //parametresiz sadece bir tane HttpGet olabilir
        //public Book GetById([FromQuery] string id)
        //{
        //    var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;


        //}

        //ekleme
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newbook)
        {
            var book = BookList.SingleOrDefault(x=>x.Title==newbook.Title);
            if (book is not null)
            {
                return BadRequest();
            }
            BookList.Add(newbook);
            return Ok();
        
        }

        //güncelleme
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedbook)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            book.GenreId=updatedbook.GenreId!=default?updatedbook.GenreId:book.GenreId;
            book.PageCount = updatedbook.PageCount != default ? updatedbook.PageCount : book.PageCount;
            book.PublishDate = updatedbook.PublishDate != default ? updatedbook.PublishDate : book.PublishDate;
            book.Title = updatedbook.Title != default ? updatedbook.Title : book.Title;

            return Ok();

        }
    }
}
