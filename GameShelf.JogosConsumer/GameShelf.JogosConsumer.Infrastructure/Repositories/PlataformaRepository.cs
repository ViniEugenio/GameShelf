using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Domain.Projections.Plataforma;
using GameShelf.JogosConsumer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.JogosConsumer.Infrastructure.Repositories
{
    public class PlataformaRepository(Context context) : BaseRepository<Plataforma>(context), IPlataformaRepository
    {
        public async Task<List<string>> FiltrarPlataformasNaoCadastradas(List<string> plataformas)
        {

            return await _context
                .Database
                .SqlQuery<string>($@"

                    declare @plataformasVerificacao table (Nome varchar(max))

                    insert into @plataformasVerificacao values {string.Join(",", plataformas)}

                    select 

	                    plataformaVerificacao.Nome 
	
                    from @plataformasVerificacao plataformaVerificacao

	                    left join Plataforma plataforma on plataformaVerificacao.Nome = plataforma.Nome

                    where 

	                    plataforma.Id is null

                ")
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
