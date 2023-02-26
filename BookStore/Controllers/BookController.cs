using BookStore.DBOperations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]

    public class BookController : ControllerBase
    {
        
        private readonly BookStoreDbContext _context; //readonly değişkenler sadece constructor içinden değiştirilebilir
        
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
    

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            return bookList;


        }

        [HttpGet("{id}")] //roottan alıyoruz.Daha doğru olan yaklaşım budur yani roottan almaktır
        public Book GetById(int id)
        {
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
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
            var book = _context.Books.SingleOrDefault(x=>x.Title==newbook.Title);
            if (book is not null)
            {
                return BadRequest();
            }
            _context.Books.Add(newbook);

            _context.SaveChanges();
            return Ok();
        
        }

        //güncelleme
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedbook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            book.GenreId=updatedbook.GenreId!=default?updatedbook.GenreId:book.GenreId;
            book.PageCount = updatedbook.PageCount != default ? updatedbook.PageCount : book.PageCount;
            book.PublishDate = updatedbook.PublishDate != default ? updatedbook.PublishDate : book.PublishDate;
            book.Title = updatedbook.Title != default ? updatedbook.Title : book.Title;
            _context.SaveChanges();
            return Ok();

        }

        //silme
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();

        }
    }
}
