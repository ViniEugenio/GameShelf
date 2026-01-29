using GameShelf.Application.ApplicationServices.Interfaces;
using MediatR;

namespace GameShelf.Application.CQRS.Commands.PublicarOutBoxMessages
{
    public class PublicarOutBoxMessagesCommandHandler(IOutboxService outboxService) : IRequestHandler<PublicarOutBoxMessagesCommand, Unit>
    {

        private readonly IOutboxService _outboxService = outboxService;

        public async Task<Unit> Handle(PublicarOutBoxMessagesCommand request, CancellationToken cancellationToken)
        {
            await _outboxService.PublicarMensagensPendentes();
            return Unit.Value;
        }
    }
}
