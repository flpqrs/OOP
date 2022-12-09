using FluentValidation;
using Library.Entities;

namespace Library.Service
{
    internal class LibraryInputValidatorService : AbstractValidator<LibraryEntity>
    {
        public LibraryInputValidatorService()
        {
            RuleFor(library => library.LibraryName)
                .NotEmpty().WithMessage("Library name must be not empty")
                .MaximumLength(100).WithMessage("Library name lenght must be less than 100 symbols.");
        }
    }
}

