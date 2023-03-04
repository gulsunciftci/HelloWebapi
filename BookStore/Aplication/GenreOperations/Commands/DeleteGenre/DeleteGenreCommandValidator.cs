using FluentValidation;

namespace BookStore.Aplication.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidator:AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0);
        }
    }
}
