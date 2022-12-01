using FluentValidation;
using Library.Entities;

namespace Library.Service
{
    internal class BookValidatorService : AbstractValidator<BookEntity>
    {
        public BookValidatorService()
        {
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title must be not empty")
                .MaximumLength(40).WithMessage("Book title lenght must be less than 40 symbols.");

            RuleFor(book => book.AuthorsFirstName)
                .NotEmpty().WithMessage("You entered empty string.")
                .Matches(@"^[a-zA-Z-']*$").WithMessage("Authors's First Name must be without special characters or numbers.")
                .MaximumLength(20).WithMessage("Author's First Name must be less than 20 symbols.");

            RuleFor(book => book.AuthorsLastName)
                .NotEmpty().WithMessage("You entered empty string.")
                .Matches(@"^[a-zA-Z-']*$").WithMessage("Authors's Last Name must be without special characters or numbers.")
                .MaximumLength(20).WithMessage("Author's Last Name must be less than 20 symbols.");

            RuleFor(book => book.Year)
                .InclusiveBetween(1, DateTime.Now.Year).WithMessage("Wrong year.");
        }

    }
}
