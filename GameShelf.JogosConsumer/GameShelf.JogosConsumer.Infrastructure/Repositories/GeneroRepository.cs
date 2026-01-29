using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Domain.Projections.RawG;
using GameShelf.JogosConsumer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.JogosConsumer.Infrastructure.Repositories
{
    public class GeneroRepository(Context context) : BaseRepository<Genero>(context), IGeneroRepository
    {
        public async Task<List<string>> FiltrarGenerosNaoCadastrados(List<RawGGenreProjection> generos)
        {

            return await _context
                .Database
                .SqlQuery<string>($@"

                    declare @generosVerificacao table (Nome varchar(max))

                    insert into @generosVerificacao values {string.Join(",", generos.Select(genero => $"('{genero.Name}')"))}

                    select 

	                    generosVerificacao.Nome 
	
                    from @generosVerificacao generoVerificacao

	                    left join Genero genero on generoVerificacao.Nome = genero.Nome

                    where 

	                    genero.Id is null

                ")
                .ToListAsync();

        }

        public async Task<List<Guid>> GetIdsGenerosFiltradosPorNome(List<string> nomesGeneros)
        {

            return await _dbSet
                .Where(genero =>

                    nomesGeneros.Contains(genero.Nome)
                    && genero.Ativo

                )
                .Select(genero => genero.Id)
                .ToListAsync();

        }

    }
}
