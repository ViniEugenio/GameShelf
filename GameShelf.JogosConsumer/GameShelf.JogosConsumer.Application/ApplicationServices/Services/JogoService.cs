using GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces;
using GameShelf.JogosConsumer.Application.DTOs.Jogos;
using GameShelf.JogosConsumer.Application.DTOs.RawG;
using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.ExternalServices;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Domain.Projections.RawG;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;

namespace GameShelf.JogosConsumer.Application.ApplicationServices.Services
{
    public class JogoService(
        IOptions<RawGConfigurationDTO> iOptionsRawGConfiguration,
        IRawGService rawGService,
        IJogoRepository jogoRepository,
        IGeneroService generoService,
        IPlataformaService plataformaService,
        IJogoGeneroService jogoGeneroService,
        IJogoPlataformaService jogoPlataformaService,
        ILogger<JogoService> logger) : IJogoService
    {

        private readonly IRawGService _rawGService = rawGService;
        private readonly RawGConfigurationDTO _rawGConfiguration = iOptionsRawGConfiguration.Value;
        private readonly IJogoRepository _jogoRepository = jogoRepository;
        private readonly IGeneroService _generoService = generoService;
        private readonly IPlataformaService _plataformaService = plataformaService;
        private readonly IJogoGeneroService _jogoGeneroService = jogoGeneroService;
        private readonly IJogoPlataformaService _jogoPlataformaService = jogoPlataformaService;
        private readonly ILogger<JogoService> _logger = logger;

        private readonly List<AtualizarJogosAuxiliarDTO> _generosAuxiliar = [];
        private readonly List<AtualizarJogosAuxiliarDTO> _plataformasAuxiliar = [];

        public async Task AtualizarJogos()
        {

            _logger.LogInformation("Iniciando de sincronização de jogos!");

            int paginaAtual = 1;
            int quantidadeJogosPorPagina = 40;

            ApiResponse<RawGListGamesResultProjection> gamesResult = await _rawGService
                .GetGames(new RawGGetGamesFilterProjection(_rawGConfiguration.Key, paginaAtual, quantidadeJogosPorPagina));

            bool requisicaoRealizadaComSucesso = gamesResult.IsSuccessStatusCode
                && gamesResult.Content.Games.Count > 0;

            if (!requisicaoRealizadaComSucesso)
            {

                _logger.LogInformation("Erro na requisição a API {StatusCode}!", gamesResult.StatusCode);
                return;

            }

            _logger.LogInformation("Quantidade de jogos: {QuantidadeTotalDeJogos}", gamesResult.Content.QuantidadeTotalDeJogos);

            int quantidadePaginas = (int)Math.Ceiling((float)gamesResult.Content.QuantidadeTotalDeJogos / quantidadeJogosPorPagina);

            while (paginaAtual < quantidadePaginas)
            {

                await AtualizarListagemAuxilarGeneros(gamesResult.Content);
                await AtualizarListagemAuxiliarPlataformas(gamesResult.Content);
                await CadastrarNovosJogos(gamesResult.Content);

                float porcentagemSincronia = MathF.Round((float)paginaAtual / quantidadePaginas * 100, 4);

                _logger.LogInformation("Sincronia em {Porcentagem}", porcentagemSincronia);

                paginaAtual++;

                int tempoEsperaRealizarNovaRequisicao = 30;
                await Task.Delay(TimeSpan.FromSeconds(tempoEsperaRealizarNovaRequisicao));

                gamesResult = await _rawGService
                    .GetGames(new RawGGetGamesFilterProjection(_rawGConfiguration.Key, paginaAtual, quantidadeJogosPorPagina));

                requisicaoRealizadaComSucesso = gamesResult.IsSuccessStatusCode
                    && gamesResult.Content.Games.Count > 0;

                if (!requisicaoRealizadaComSucesso)
                {

                    _logger.LogInformation("Erro na requisição a API {StatusCode}!", gamesResult.StatusCode);
                    break;

                }

            }

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
            List<AtualizarJogosAuxiliarDTO> novasPlataformasCadastradas = await _plataformaService.CadastrarNovasPlataformas(plataformasAindaNaoCadastradas);
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

            List<JogoGenero> generosJogos = [];
            List<JogoPlataforma> plataformasJogos = [];

            List<Jogo> jogos = [..

                gameResult
                    .Games
                    .Select(game=>
                    {

                        Guid idJogo = Guid.NewGuid();

                        Jogo novoJogo = new()
                        {
                            Id = idJogo,
                            Nome = game.Name,
                            Descricao = game.Slug,
                            Imagem = game.Image,
                        };

                        generosJogos.AddRange(GetGenerosJogoParaCadastro(idJogo, game));
                        plataformasJogos.AddRange(GetPlataformasJogoParaCadastro(idJogo, game));

                        return novoJogo;

                    })

            ];

            if (jogos.Count == 0)
            {
                return;
            }

            await _jogoRepository.BulkingInsert(jogos);
            await _jogoGeneroService.RelacionarGenerosJogos(generosJogos);
            await _jogoPlataformaService.RelacionarPlataformasJogos(plataformasJogos);

        }

        private List<JogoGenero> GetGenerosJogoParaCadastro(Guid idJogoCadastrado, RawGGameProjection game)
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
                        Id = Guid.NewGuid(),
                        JogoId = idJogoCadastrado,
                        GeneroId = generoAuxiliar.Id,
                        DataAtivacao = DateTime.Now,
                        Ativo = true
                    })

            ];

        }

        private List<JogoPlataforma> GetPlataformasJogoParaCadastro(Guid idJogoCadastrado, RawGGameProjection game)
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
                        Id = Guid.NewGuid(),
                        JogoId = idJogoCadastrado,
                        PlataformaId = generoAuxiliar.Id,
                        DataAtivacao = DateTime.Now,
                        Ativo = true
                    })

            ];

        }

    }
}
