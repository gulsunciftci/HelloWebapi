using AutoMapper;
using BookStore.Aplication.GenreOperations.Commands.CreateGenre;
using BookStore.Aplication.GenreOperations.Commands.DeleteGenre;
using BookStore.Aplication.GenreOperations.Commands.UpdateGenre;
using BookStore.Aplication.GenreOperations.Queries.GetGenreDetail;
using BookStore.Aplication.GenreOperations.Queries.GetGenres;
using BookStore.BookOperations.GetBooks;
using BookStore.DBOperations;
using BookStore.DeleteBook;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController:ControllerBase
    {
        public readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var  obj= query.Handle();
            return Ok(obj);

            
        }
        
        [HttpGet("{id}")] 
        public IActionResult GetGenreDetail(int id)
        {
            GenreDetailViewModel obj;
            try
            {
                GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
                query.GenreId = id;
                GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
                validator.ValidateAndThrow(query);

                obj = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(obj);
        }


        
        //ekleme
        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
           
            command.Model = newGenre;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

            validator.ValidateAndThrow(command);
            command.Handle();

           

            return Ok();
           

        }

        //güncelleme
        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updatedgenre)
        {
            

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = id;
            command.Model = updatedgenre;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();


            return Ok();

        }

        //silme
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
           

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.Validate(command);
            command.Handle();

            return Ok();

        }

    }
}
