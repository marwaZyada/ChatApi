using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.Message.Command.AddMessage
{
    public class AddMessageCommandValidator:AbstractValidator<AddMessageCommand>
    {
        public AddMessageCommandValidator() 
        {
            RuleFor(x=>x.Content).NotNull()
                .WithMessage("{PropertyName} must be not null")
                .MinimumLength(3).WithMessage("{PropertyName} must be min lenth {PropertyValue}");
        }
    }
}
