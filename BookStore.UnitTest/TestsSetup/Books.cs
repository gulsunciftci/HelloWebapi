using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTest.TestsSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book
                {

                    Title = "Lean Startup",
                    GenreId = 1,
                    PageCount = 200,
                    PublishDate = new System.DateTime(2001, 06, 12)
                },
                new Book
                {

                    Title = "Herland",
                    GenreId = 2,
                    PageCount = 250,
                    PublishDate = new System.DateTime(2010, 05, 23)
                },
                new Book
                {

                    Title = "Dune",
                    GenreId = 2,
                    PageCount = 540,
                    PublishDate = new System.DateTime(2001, 12, 21)
                });
        }
    }
}
