using GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces;
using GameShelf.JogosConsumer.Application.DTOs.Jogos;
using GameShelf.JogosConsumer.Application.DTOs.RawG;
using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.ExternalServices;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Domain.Projections.RawG;
using Microsoft.Extensions.Options;

namespace GameShelf.JogosConsumer.Application.ApplicationServices.Services
{
    public class JogoService(
        IOptions<RawGConfigurationDTO> iOptionsRawGConfiguration,
        IRawGService rawGService,
        IJogoRepository jogoRepository,
        IGeneroService generoService,
        IPlataformaService plataformaService) : IJogoService
    {

        private readonly IRawGService _rawGService = rawGService;
        private readonly RawGConfigurationDTO _rawGConfiguration = iOptionsRawGConfiguration.Value;
        private readonly IJogoRepository _jogoRepository = jogoRepository;
        private readonly IGeneroService _generoService = generoService;
        private readonly IPlataformaService _plataformaService = plataformaService;

        public async Task AtualizarJogos()
        {

            List<Jogo> jogosParaCadastro = [];



            //RawGListGamesResultProjection gamesResult = await _rawGService
            //    .GetGames(new RawGGetGamesFilterProjection(_rawGConfiguration.Key, paginaAtual, quantidadeJogosPorPagina));

            //if (gamesResult.Games.Count == 0)
            //{
            //    return;
            //}

            //int quantidadeTotalPaginas = (int)Math.Floor((decimal)gamesResult.QuantidadeTotalDeJogos / quantidadeJogosPorPagina);

            //while (quantidadeJogosPorPagina <= paginaAtual)
            //{

            //}

        }

        private async Task<int> HandlePacoteInformacoesAindaNaoCadastradas(int ultimaPaginaUtilizada)
        {

            Task<InfosAindaNaoCadastradasDTO>[] acoes =
            [
                GetInfosAindaNaoCadastradas(ultimaPaginaUtilizada + 1),
                GetInfosAindaNaoCadastradas(ultimaPaginaUtilizada + 2),
                GetInfosAindaNaoCadastradas(ultimaPaginaUtilizada + 3),
                GetInfosAindaNaoCadastradas(ultimaPaginaUtilizada + 4),
                GetInfosAindaNaoCadastradas(ultimaPaginaUtilizada + 5)
            ];

            List<InfosAindaNaoCadastradasDTO> infosAindaNaoCadastradas = [..

                (await Task.WhenAll(acoes))

            ];

            await _generoService
                .CadastrarNovosGeneros(infosAindaNaoCadastradas);

            await _plataformaService
                .CadastrarNovasPlataformas(infosAindaNaoCadastradas);

            await CadastrarNovosJogos(infosAindaNaoCadastradas);

            return ultimaPaginaUtilizada + 5;

        }

        private async Task<InfosAindaNaoCadastradasDTO> GetInfosAindaNaoCadastradas(int paginaAtual)
        {

            int quantidadeJogosPorPagina = 40;

            RawGListGamesResultProjection gamesResult = await _rawGService
                .GetGames(new RawGGetGamesFilterProjection(_rawGConfiguration.Key, paginaAtual, quantidadeJogosPorPagina));

            if (gamesResult.Games.Count == 0)
            {
                return new();
            }

            List<string> jogosAindaNaoCadastrados = await _jogoRepository
                .FiltrarJogosNaoCadastrados(gamesResult.Games);

            List<RawGGenreProjection> generosParaVerificacao = [..

                gamesResult
                    .Games
                    .SelectMany(game => game.Generos)

            ];

            List<string> generosAindaNaoCadastrados = await _generoService
                .FiltrarGenerosAindaNaoCadastrados(generosParaVerificacao);

            List<RawGPlatformDetailsProjection> plataformasParaVerificacao = [..
                gamesResult
                    .Games
                    .SelectMany(game => game.Plataformas)
            ];

            List<string> plataformasAindaNaoCadastradas = await _plataformaService
                .FiltrarPlataformasAindaNaoCadastradas(plataformasParaVerificacao);

            return new
            (
                gamesResult.Games,
                jogosAindaNaoCadastrados,
                generosAindaNaoCadastrados,
                plataformasAindaNaoCadastradas
            );

        }

        private async Task CadastrarNovosJogos(List<InfosAindaNaoCadastradasDTO> infosAindaNaoCadastradas)
        {

            bool existemNovoJogosSeremCadastrados = infosAindaNaoCadastradas
                .Any(info => info.JogosAindaNaoCadastrados.Count > 0);

            if (!existemNovoJogosSeremCadastrados)
            {
                return;
            }

            List<Guid> idsGenerosJogos = await _generoService
                .GetIdsGenerosFiltradosPorNome(infosAindaNaoCadastradas);

        }

    }
}
