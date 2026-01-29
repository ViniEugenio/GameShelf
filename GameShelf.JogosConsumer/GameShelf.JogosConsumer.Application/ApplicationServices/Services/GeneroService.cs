using GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces;
using GameShelf.JogosConsumer.Application.DTOs.Jogos;
using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Domain.Projections.RawG;

namespace GameShelf.JogosConsumer.Application.ApplicationServices.Services
{
    public class GeneroService(IGeneroRepository generoRepository) : IGeneroService
    {

        private readonly IGeneroRepository _generoRepository = generoRepository;

        public async Task CadastrarNovosGeneros(List<InfosAindaNaoCadastradasDTO> infosAindaNaoCadastradas)
        {

            List<Genero> novosGeneros = [..

                infosAindaNaoCadastradas
                    .SelectMany(info => info.GenerosAindaNaoCadastrados)
                    .Distinct()
                    .Select(genero => new Genero()
                    {
                        Nome = genero,
                        Ativo = true,
                        DataAtivacao = DateTime.UtcNow
                    })

            ];

            if (novosGeneros.Count == 0)
            {
                return;
            }

            await _generoRepository
                .BulkingInsert(novosGeneros);

        }

        public async Task<List<string>> FiltrarGenerosAindaNaoCadastrados(List<RawGGenreProjection> generos)
        {
            return await _generoRepository.FiltrarGenerosNaoCadastrados(generos);
        }

        public async Task<List<Guid>> GetIdsGenerosFiltradosPorNome(List<InfosAindaNaoCadastradasDTO> infosAindaNaoCadastradas)
        {

            List<string> novosGeneros = [..

                infosAindaNaoCadastradas
                    .SelectMany(info => info.GenerosAindaNaoCadastrados)
                    .Distinct()

            ];

            if (novosGeneros.Count == 0)
            {
                return [];
            }

            return await _generoRepository
                .GetIdsGenerosFiltradosPorNome(novosGeneros);

        }
    }
}
