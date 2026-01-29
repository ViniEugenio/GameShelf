using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.DTOs;
using GameShelf.Application.DTOs.MessageBusDTO;
using GameShelf.Application.DTOs.Messages;
using GameShelf.Application.Helpers;
using GameShelf.Application.Mappings;
using GameShelf.Domain.Entities;
using GameShelf.Domain.Enums;
using GameShelf.Domain.Interfaces.ExternalServicesInterfaces;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;
using GameShelf.Domain.Models.Projections.Outbox;
using System.Text.Json;

namespace GameShelf.Application.ApplicationServices.Services
{
    public class OutboxService(IOutboxRepository outboxRepository, IMessageBus messageBus) : IOutboxService
    {

        private readonly IOutboxRepository _outboxRepository = outboxRepository;
        private readonly IMessageBus _messageBus = messageBus;

        public async Task<ResponseDTO> AddOutboxRequisitionAtualizarJogos()
        {

            AtualizarJogosMessageDTO payload = new()
            {
                EventId = Guid.NewGuid(),
                Mensagem = "Atualizar jogos!"
            };

            ExchangeSetupDTO exchange = QueuesFactory.GetExchangeByIdentifier(EExchangeIdentifier.AtualizarJogos);

            Outbox novaRequisicaoMensagem = new()
            {
                Payload = JsonSerializer.Serialize(payload),
                EventId = payload.EventId,
                Exchange = exchange.Name,
                RoutingKey = exchange.Queues[0].RoutingKey,
                MessageStatus = EMessageStatus.Pendente,
                DataCriacao = DateTime.Now
            };

            await _outboxRepository.Add(novaRequisicaoMensagem);

            ResponseDTO response = new();

            response
                .AddData(CommonMappings.MapNewRegister(novaRequisicaoMensagem));

            return response;

        }

        public async Task PublicarMensagensPendentes()
        {

            List<OutboxPendingMessageProjection> mensagensPendentes = await _outboxRepository.GetPendingMessages();
            if (mensagensPendentes.Count == 0)
            {
                return;
            }

            List<Guid> idsMensagensPublicadas = [];
            foreach (OutboxPendingMessageProjection mensagem in mensagensPendentes)
            {

                await _messageBus.Publish(mensagem);
                idsMensagensPublicadas.Add(mensagem.Id);

            }

            if (idsMensagensPublicadas.Count == 0)
            {
                return;
            }

            await _outboxRepository.MarcarMensagensComoPublicadas(idsMensagensPublicadas);

        }

    }
}
