using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.CQRS.Commands.CriarPrateleira;
using GameShelf.Domain.Entities;

namespace GameShelf.Application.Mappings
{
    public static class PrateleiraMappings
    {

        public static Prateleira MapNovaPrateleira(CriarPrateleiraCommand command, ISessao sessao)
        {

            return new Prateleira()
            {
                UserId = sessao.GetUsuarioLogado().Id,
                Nome = command.Nome,
                Participantes = [..
                    command
                    .Participantes
                    .Select(participante => new ParticipantePrateleira()
                    {
                        UserId = participante
                    })

                ]
            };

        }

    }
}