using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Domain.Projections.Genero;
using GameShelf.JogosConsumer.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GameShelf.JogosConsumer.Infrastructure.Repositories
{
    public class GeneroRepository(Context context) : BaseRepository<Genero>(context), IGeneroRepository
    {
        public async Task<List<string>> FiltrarGenerosNaoCadastrados(List<string> generos)
        {

            string generosJson = JsonSerializer.Serialize(generos);

            string query = @"

                select 

                    generoVerificacao.value

                from OPENJSON(@generosJson) generoVerificacao
                left join Genero genero on generoVerificacao.value = genero.Nome

                where

                    genero.Id is null

            ";

            return await _context
                .Database
                .SqlQueryRaw<string>(query, new SqlParameter("@generosJson", generosJson))
                .ToListAsync();

        }

        public async Task<List<GeneroJaCadastradoProjection>> GetGenerosFiltradosPorNome(List<string> nomesGeneros)
        {

            return await _dbSet
                .Where(genero =>

                    nomesGeneros.Contains(genero.Nome)
                    && genero.Ativo

                )
                .Select(genero => new GeneroJaCadastradoProjection(genero.Id, genero.Nome))
                .ToListAsync();

        }

    }
}
