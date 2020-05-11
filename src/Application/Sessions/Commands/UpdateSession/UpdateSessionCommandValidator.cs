using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace BoxOffice.Application.Sessions.Commands.UpdateSession
{
    public class UpdateSessionCommandValidator : AbstractValidator<UpdateSessionCommand>
    {
        public UpdateSessionCommandValidator()
        {
            RuleFor(v => v.Time)
                .NotEmpty();
            RuleFor(v => v.PlacesLimit)
                .GreaterThan(0)
                .NotEmpty();
        }
    }
}
