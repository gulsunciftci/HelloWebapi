using BookStore.DBOperations;
using System;
using System.Linq;

namespace BookStore.Aplication.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _context;

        public DeleteGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı");

            }
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }


    }
}
