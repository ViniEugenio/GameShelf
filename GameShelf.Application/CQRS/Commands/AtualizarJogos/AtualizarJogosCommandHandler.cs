using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Application.DTOs;
using GameShelf.Domain.Interfaces.ExternalServicesInterfaces;
using GameShelf.Domain.Models.Filters.RawG;
using GameShelf.Domain.Models.Projections.RawG;
using MediatR;

namespace GameShelf.Application.CQRS.Commands.AtualizarJogos
{
    public class AtualizarJogosCommandHandler(
        IRawGService rawGService,
        IJogoService jogoService,
        IGeneroService generoService,
        IPlataformaService plataformaService) : IRequestHandler<AtualizarJogosCommand, ResponseDTO>
    {

        private readonly IRawGService _rawGService = rawGService;
        private readonly IJogoService _jogoService = jogoService;
        private readonly IGeneroService _generoService = generoService;
        private readonly IPlataformaService _plataformaService = plataformaService;

        public async Task<ResponseDTO> Handle(AtualizarJogosCommand request, CancellationToken cancellationToken)
        {

            ResponseDTO response = new();

            int paginaAtual = 1;
            int quantidadeJogosPorPagina = 40;

            GetGamesFilter filtro = new(paginaAtual, quantidadeJogosPorPagina);

            RawGListGamesResultProjection projection = await _rawGService
                .GetGames(filtro);

            if (!projection.Success)
            {

                response.AdicionarErros(AtualizarJogosErros.ErroAoAtualizarJogos);
                return response;

            }

            filtro.page++;

            bool realizarNovaRequisicao = projection.Success
                && filtro.page <= projection.QuantidadeTotalPaginas;

            while (realizarNovaRequisicao)
            {

                projection = await _rawGService
                    .GetGames(filtro);

                filtro.page++;

                realizarNovaRequisicao = projection.Success
                    && filtro.page <= projection.QuantidadeTotalPaginas;

            }

            return response;

        }

    }
}