using FluentValidation;
using System;

namespace BookStore.BookOperations.CreateBook
{
    public class CreateBookCommandValidator:AbstractValidator<CreateBookCommand>
    {
        //CreateBookCommandValidator sınıfı CreateBookCommand ın nesnelerini valide eder
         
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0); //GreaterThan vermiş olduğunuz parametre değerden daha büyük olmasını garanti eder
            RuleFor(commad => commad.Model.PageCount).GreaterThan(0);
            RuleFor(commad => commad.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(commad => commad.Model.Title).NotEmpty().MinimumLength(4);


        }
    
    
    }
}
