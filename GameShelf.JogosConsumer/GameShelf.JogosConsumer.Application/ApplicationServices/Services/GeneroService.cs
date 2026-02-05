using GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces;
using GameShelf.JogosConsumer.Application.DTOs.Jogos;
using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Domain.Projections.Genero;

namespace GameShelf.JogosConsumer.Application.ApplicationServices.Services
{
    public class GeneroService(IGeneroRepository generoRepository) : IGeneroService
    {

        private readonly IGeneroRepository _generoRepository = generoRepository;

        public async Task<List<AtualizarJogosAuxiliarDTO>> CadastrarNovosGeneros(List<string> generos)
        {

            if (generos.Count == 0)
            {
                return [];
            }

            List<Genero> novosGeneros = [..

                generos
                    .Distinct()
                    .Select(genero => new Genero()
                    {
                        Id = Guid.NewGuid(),
                        Nome = genero,
                        Ativo = true,
                        DataAtivacao = DateTime.UtcNow
                    })

            ];

            await _generoRepository
                .BulkingInsert(novosGeneros);

            return [..

                novosGeneros
                    .Select(genero => new AtualizarJogosAuxiliarDTO(genero.Id, genero.Nome))

            ];

        }

        public async Task<List<string>> FiltrarGenerosAindaNaoCadastrados(List<string> generos)
        {
            return await _generoRepository.FiltrarGenerosNaoCadastrados(generos);
        }

        public async Task<List<AtualizarJogosAuxiliarDTO>> GetGenerosFiltradosPorNome(List<string> generos)
        {

            if (generos.Count == 0)
            {
                return [];
            }

            generos = [..
                generos
                    .Distinct()
            ];

            List<GeneroJaCadastradoProjection> projection = await _generoRepository
                .GetGenerosFiltradosPorNome(generos);

            return [..

                projection
                    .Select(genero => new AtualizarJogosAuxiliarDTO(genero.Id, genero.Name))

            ];

        }
    }
}
