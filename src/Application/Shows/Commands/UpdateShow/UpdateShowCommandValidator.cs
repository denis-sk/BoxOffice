using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace BoxOffice.Application.Shows.Commands.UpdateShow
{
    public class UpdateShowCommandValidator : AbstractValidator<UpdateShowCommand>
    {
        public UpdateShowCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(255)
                .NotEmpty();
        }
    }
}
