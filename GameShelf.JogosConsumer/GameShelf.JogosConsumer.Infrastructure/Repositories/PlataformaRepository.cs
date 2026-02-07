using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Domain.Projections.Plataforma;
using GameShelf.JogosConsumer.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GameShelf.JogosConsumer.Infrastructure.Repositories
{
    public class PlataformaRepository(Context context) : BaseRepository<Plataforma>(context), IPlataformaRepository
    {
        public async Task<List<string>> FiltrarPlataformasNaoCadastradas(List<string> plataformas)
        {

            string jsonPlataformas = JsonSerializer.Serialize(plataformas);
            string query = @"

                select

                    plataformaJson.value

                from OPENJSON(@plataformaJson) plataformaJson

                left join Plataforma plataforma on plataformaJson.value = plataforma.Nome

                where 

                    plataforma.Id is null

            ";

            return await _context
                .Database
                .SqlQueryRaw<string>(query, new SqlParameter("@plataformaJson", jsonPlataformas))
                .ToListAsync();

        }

        public async Task<List<PlataformaJaCadastradaProjection>> GetPlataformasFiltradasPorNome(List<string> plataformas)
        {

            return await _dbSet
                .Where(plataforma =>

                    plataformas.Contains(plataforma.Nome)
                    && plataforma.Ativo

                )
                .Select(plataforma => new PlataformaJaCadastradaProjection(plataforma.Id, plataforma.Nome))
                .ToListAsync();

        }
    }
}
