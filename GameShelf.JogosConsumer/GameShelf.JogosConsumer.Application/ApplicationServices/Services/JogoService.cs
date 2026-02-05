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

        private readonly List<AtualizarJogosAuxiliarDTO> _generosAuxiliar = [];
        private readonly List<AtualizarJogosAuxiliarDTO> _plataformasAuxiliar = [];

        public async Task AtualizarJogos()
        {

            int paginaAtual = 0;
            int quantidadeJogosPorPagina = 40;

            RawGListGamesResultProjection gamesResult = await _rawGService
                .GetGames(new RawGGetGamesFilterProjection(_rawGConfiguration.Key, paginaAtual + 1, quantidadeJogosPorPagina));

            if (gamesResult.Games.Count == 0)
            {
                return;
            }

            int quantidadePaginas = (int)Math.Ceiling((float)gamesResult.QuantidadeTotalDeJogos / quantidadeJogosPorPagina);

            while (paginaAtual < quantidadePaginas)
            {

                paginaAtual++;

                await AtualizarListagemAuxilarGeneros(gamesResult);
                await AtualizarListagemAuxiliarPlataformas(gamesResult);
                await CadastrarNovosJogos(gamesResult);

            }

            await _jogoRepository.SaveChanges();

        }

        private async Task AtualizarListagemAuxilarGeneros(RawGListGamesResultProjection gameResult)
        {

            List<string> generos = [..

                gameResult
                    .Games
                    .SelectMany(game =>

                        game
                            .Generos
                            .Select(genero => genero.Name)

                    )
                    .Distinct()
                    .Where(genero =>

                        !_generosAuxiliar
                            .Select(generoAuxiliar => generoAuxiliar.Name)
                            .Contains(genero)

                    )

            ];

            if (generos.Count == 0)
            {
                return;
            }

            List<string> generosAindaNaoCadastrados = await _generoService.FiltrarGenerosAindaNaoCadastrados(generos);
            List<AtualizarJogosAuxiliarDTO> novosGenerosCadastrados = await _generoService.CadastrarNovosGeneros(generosAindaNaoCadastrados);
            _generosAuxiliar.AddRange(novosGenerosCadastrados);

            List<string> nomesGenerosJaCadastrados = [..

                generos
                    .Where(genero => !generosAindaNaoCadastrados.Contains(genero))

            ];

            List<AtualizarJogosAuxiliarDTO> generosJaCadastrados = await _generoService.GetGenerosFiltradosPorNome(nomesGenerosJaCadastrados);
            _generosAuxiliar.AddRange(generosJaCadastrados);

        }

        private async Task AtualizarListagemAuxiliarPlataformas(RawGListGamesResultProjection gameResult)
        {

            List<string> plataformas = [..

                gameResult
                    .Games
                    .SelectMany(game =>

                        game
                            .Plataformas
                            .Select(plataforma => plataforma.Platform.Name)

                    )
                    .Distinct()
                    .Where(plataforma =>

                        !_plataformasAuxiliar
                            .Select(plataforma => plataforma.Name)
                            .Contains(plataforma)

                    )

            ];

            if (plataformas.Count == 0)
            {
                return;
            }

            List<string> plataformasAindaNaoCadastradas = await _plataformaService.FiltrarPlataformasAindaNaoCadastradas(plataformas);
            List<AtualizarJogosAuxiliarDTO> novasPlataformasCadastradas = await _plataformaService.CadastrarNovasPlataformas(plataformas);
            _plataformasAuxiliar.AddRange(novasPlataformasCadastradas);

            List<string> nomesPlataformasJaCadastradas = [..

                plataformas
                    .Where(plataforma => !plataformasAindaNaoCadastradas.Contains(plataforma))

            ];

            List<AtualizarJogosAuxiliarDTO> plataformasJaCadastradas = await _plataformaService.GetPlataformasFiltradasPorNome(nomesPlataformasJaCadastradas);
            _plataformasAuxiliar.AddRange(plataformasJaCadastradas);

        }

        private async Task CadastrarNovosJogos(RawGListGamesResultProjection gameResult)
        {

            List<Jogo> jogos = [..

                gameResult
                    .Games
                    .Select(game=> new Jogo()
                    {
                        Nome = game.Name,
                        Descricao = game.Slug,
                        Imagem = game.Image,
                        Generos = GetGenerosJogoParaCadastro(game),
                        Plataformas = GetPlataformasJogoParaCadastro(game)
                    })

            ];

            if (jogos.Count == 0)
            {
                return;
            }

            await _jogoRepository
                .BulkingInsert(jogos);

        }

        private List<JogoGenero> GetGenerosJogoParaCadastro(RawGGameProjection game)
        {

            return [..

                _generosAuxiliar
                    .Where(generoAuxiliar =>

                        game
                            .Generos
                            .Select(genero => genero.Name)
                            .Contains(generoAuxiliar.Name)

                    )
                    .Select(generoAuxiliar => new JogoGenero()
                    {
                        GeneroId = generoAuxiliar.Id
                    })

            ];

        }

        private List<JogoPlataforma> GetPlataformasJogoParaCadastro(RawGGameProjection game)
        {

            return [..

                _plataformasAuxiliar
                    .Where(plataformaAuxiliar =>

                        game
                            .Plataformas
                            .Select(plataforma => plataforma.Platform.Name)
                            .Contains(plataformaAuxiliar.Name)

                    )
                    .Select(generoAuxiliar => new JogoPlataforma()
                    {
                        PlataformaId = generoAuxiliar.Id
                    })

            ];

        }

    }
}
