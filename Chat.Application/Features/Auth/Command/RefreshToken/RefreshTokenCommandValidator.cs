using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandValidator:
        FluentValidation.AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required.")
                .NotNull().WithMessage("Refresh token cannot be null.");
        }

    }
}
