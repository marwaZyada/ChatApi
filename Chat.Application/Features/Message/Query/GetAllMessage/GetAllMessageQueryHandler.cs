using AutoMapper;
using Chat.Application.Contracts;
using Chat.Application.Features.Message.Dto;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.Message.Query.GetAllMessage
{
    public class GetAllMessageQueryHandler : IRequestHandler<GetAllMessageQuery, List<MessageReturnDto>>
    {
        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllMessageQuery> _logger;

        public GetAllMessageQueryHandler(IMessageRepository repository,IMapper mapper,ILogger<GetAllMessageQuery> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<MessageReturnDto>> Handle(GetAllMessageQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                var messages = await _repository.GetAllAsync();
                return _mapper.Map<List<MessageReturnDto>>(messages);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex,"No message found");
                throw;
            }
        }
    }
}
