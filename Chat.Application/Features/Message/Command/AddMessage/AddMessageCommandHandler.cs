using Chat.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.Message.Command.AddMessage
{
    public class AddMessageCommandHandler : IRequestHandler<AddMessageCommand, Unit>
    {
        private readonly IMessageRepository _repository;

        public AddMessageCommandHandler(IMessageRepository repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Domain.Entities.Message
            {
                
                Content = request.Content,
                SenderId =Guid.Parse( request.SenderId),
                RecepientId = Guid.Parse(request.RecepientId),
                
            };
            await _repository.AddAsync(message);
            await _repository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
