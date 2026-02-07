using GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces;
using GameShelf.JogosConsumer.Application.DTOs.Jogos;
using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Domain.Projections.Plataforma;

namespace GameShelf.JogosConsumer.Application.ApplicationServices.Services
{
    public class PlataformaService(IPlataformaRepository plataformaRepository) : IPlataformaService
    {

        private readonly IPlataformaRepository _plataformaRepository = plataformaRepository;

        public async Task<List<AtualizarJogosAuxiliarDTO>> CadastrarNovasPlataformas(List<string> plataformas)
        {

            if (plataformas.Count == 0)
            {
                return [];
            }

            List<Plataforma> novasPlataformas = [..

                plataformas
                    .Distinct()
                    .Select(plataforma => new Plataforma()
                    {
                        Id = Guid.NewGuid(),
                        Nome = plataforma,
                        Ativo = true,
                        DataAtivacao = DateTime.UtcNow
                    })

            ];

            await _plataformaRepository
                .BulkingInsert(novasPlataformas);

            return [..

                novasPlataformas
                    .Select(plataforma => new AtualizarJogosAuxiliarDTO(plataforma.Id, plataforma.Nome))

            ];

        }

        public async Task<List<string>> FiltrarPlataformasAindaNaoCadastradas(List<string> plataformas)
        {
            return await _plataformaRepository.FiltrarPlataformasNaoCadastradas(plataformas);
        }

        public async Task<List<AtualizarJogosAuxiliarDTO>> GetPlataformasFiltradasPorNome(List<string> plataformas)
        {

            if (plataformas.Count == 0)
            {
                return [];
            }

            plataformas = [..
                plataformas
                    .Distinct()
            ];

            List<PlataformaJaCadastradaProjection> projection = await _plataformaRepository
                .GetPlataformasFiltradasPorNome(plataformas);

            return [..

                projection
                    .Select(plataforma => new AtualizarJogosAuxiliarDTO(plataforma.Id, plataforma.Name))

            ];

        }

    }
}
