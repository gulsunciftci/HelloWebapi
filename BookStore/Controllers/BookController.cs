using AutoMapper;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.GetBooks;
using BookStore.DBOperations;
using BookStore.DeleteBook;
using BookStore.GetBookDetail;
using BookStore.UpdateBook;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.UpdateBook.UpdateBookCommand;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]

    public class BookController : ControllerBase
    {

        private readonly IBookStoreDbContext _context;
        //private readonly BookStoreDbContext _context; //readonly değişkenler sadece constructor içinden değiştirilebilir
        private readonly IMapper _mapper;
        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
    

        [HttpGet]
        public IActionResult GetBooks()
        {
            //var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            //return bookList;
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);


        }

        [HttpGet("{id}")] //roottan alıyoruz.Daha doğru olan yaklaşım budur yani roottan almaktır
        public IActionResult GetById(int id)
        {
            //var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            //return book;


            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
                query.BookId = id;
                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);

                result=query.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }


        //[HttpGet] //parametresiz sadece bir tane HttpGet olabilir
        //public Book GetById([FromQuery] string id)
        //{
        //    var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;


        //}

        //ekleme
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            //Buradaki try catchi CustomExceptionMiddleware içine yazdım

            //try
            //{
                command.Model = newBook;

                CreateBookCommandValidator validator=new CreateBookCommandValidator();

                validator.ValidateAndThrow(command);
                command.Handle();

                //ValidationResult result = validator.Validate(command);

                //if (!result.IsValid)
                //{
                //    foreach (var item in result.Errors)
                //    {
                //        Console.WriteLine("Özellik-" + item.PropertyName + "- Error Message" + item.ErrorMessage);
                //    }
                //}
                //else
                //{

                //    command.Handle();
                //}

            //}

            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}


            return Ok();
            //var book = _context.Books.SingleOrDefault(x=>x.Title==newbook.Title);
            //if (book is not null)
            //{
            //    return BadRequest();
            //}
            //_context.Books.Add(newbook);

            //_context.SaveChanges();
            //return Ok();
        
        }

        //güncelleme
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedbook)
        {
            //var book = _context.Books.SingleOrDefault(x => x.Id == id);
            //if (book is null)
            //{
            //    return BadRequest();
            //}
            //book.GenreId=updatedbook.GenreId!=default?updatedbook.GenreId:book.GenreId;
            //book.PageCount = updatedbook.PageCount != default ? updatedbook.PageCount : book.PageCount;
            //book.PublishDate = updatedbook.PublishDate != default ? updatedbook.PublishDate : book.PublishDate;
            //book.Title = updatedbook.Title != default ? updatedbook.Title : book.Title;

           
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedbook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();


            return Ok();

        }

        //silme
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            //var book = _context.Books.SingleOrDefault(x => x.Id == id);
            //if (book is null)
            //{
            //    return BadRequest();
            //}
            //_context.Books.Remove(book);
            //_context.SaveChanges();

            
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.Validate(command);
                command.Handle();
           
            return Ok();

        }
    }
}
