using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Entities
{
    public class Book
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Idlerimizin otomatik gelmesi için yaptık
        public int Id { get; set; }

        public string Title { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

    }
}
