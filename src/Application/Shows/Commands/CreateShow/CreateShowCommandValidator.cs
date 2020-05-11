using FluentValidation;

namespace BoxOffice.Application.Shows.Commands.CreateShow
{
    public class CreateShowCommandValidator : AbstractValidator<CreateShowCommand>
    {
        public CreateShowCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(255)
                .NotEmpty();
        }
    }
}
