using BookStore.DBOperations;
using System;
using System.Linq;

namespace BookStore.Aplication.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public UpdateGenreModel Model { get; set; }
        public int GenreId { get; set; }
        public readonly IBookStoreDbContext _context;

        public UpdateGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Kitap türü bulunamadı");
            }
            if (_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id!=GenreId ))
            {
                throw new InvalidOperationException("Aynı isimde bir kitap türü zaten var");
            }

            genre.Name = String.IsNullOrEmpty(Model.Name) ? genre.Name:Model.Name ;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
    }
    public class UpdateGenreModel
    {

        public string Name { get; set; }
        public bool IsActive { get; set; }

    }
}
