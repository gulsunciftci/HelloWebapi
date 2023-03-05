using BookStore.BookOperations.CreateBook;
using BookStore.UnitTest.TestsSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.UnitTest.Application.BookOperations.Commands.CreateCommand
{
    public class CreateBookCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The Rings",0,0)]
        [InlineData("Lord Of The Rings", 0, 1)]
        [InlineData("Lord Of The Rings", 100, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("Lor", 100, 1)]
        [InlineData("Lord", 100, 0)]
        [InlineData("Lord", 0, 1)]
        [InlineData("", 100, 1)]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId )
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookCommand.CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };

            //act
            CreateBookCommandValidator validatior = new CreateBookCommandValidator();
            var  result=validatior.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookCommand.CreateBookModel()
            {
                Title = "Lord Of The Rings ",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            //act
            CreateBookCommandValidator validatior = new CreateBookCommandValidator();
            var result = validatior.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);


        }

      

    }
}
