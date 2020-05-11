using FluentValidation;

namespace BoxOffice.Application.Sessions.Commands.CreateSession
{
    public class CreateSessionCommandValidator : AbstractValidator<CreateSessionCommand>
    {
        public CreateSessionCommandValidator()
        {
            RuleFor(v => v.Time)
                .NotEmpty();
            RuleFor(v => v.ShowId)
                .NotEmpty();
            RuleFor(v => v.PlacesLimit)
                .GreaterThan(0)
                .NotEmpty();
        }
    }
}
