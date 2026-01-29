using GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces;
using GameShelf.JogosConsumer.Application.DTOs.Jogos;
using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Domain.Projections.RawG;

namespace GameShelf.JogosConsumer.Application.ApplicationServices.Services
{
    public class PlataformaService(IPlataformaRepository plataformaRepository) : IPlataformaService
    {

        private readonly IPlataformaRepository _plataformaRepository = plataformaRepository;

        public async Task CadastrarNovasPlataformas(List<InfosAindaNaoCadastradasDTO> infosAindaNaoCadastradas)
        {

            List<Plataforma> novasPlataformas = [..

                infosAindaNaoCadastradas
                    .SelectMany(info => info.PlataformasAindaNaoCadastradas)
                    .Distinct()
                    .Select(plataforma => new Plataforma()
                    {
                        Nome = plataforma
                    })

            ];

            if (novasPlataformas.Count == 0)
            {
                return;
            }

            await _plataformaRepository
                .BulkingInsert(novasPlataformas);

        }

        public async Task<List<string>> FiltrarPlataformasAindaNaoCadastradas(List<RawGPlatformDetailsProjection> plataformas)
        {
            return await _plataformaRepository.FiltrarPlataformasNaoCadastradas(plataformas);
        }
    }
}
