using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Domain.Projections.RawG;
using GameShelf.JogosConsumer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.JogosConsumer.Infrastructure.Repositories
{
    public class JogoRepository(Context context) : BaseRepository<Jogo>(context), IJogoRepository
    {
        public async Task<List<string>> FiltrarJogosNaoCadastrados(List<RawGGameProjection> jogos)
        {

            return await _context
                .Database
                .SqlQuery<string>($@"

                    declare @jogosVerificacao table (Nome varchar(max))

                    insert into @jogosVerificacao values {string.Join(",", jogos.Select(jogo => $"('{jogo.Name}')"))}

                    select 

	                    jogoVerificacao.Nome 
	
                    from @jogosVerificacao jogoVerificacao

	                    left join Jogo jogo on jogoVerificacao.Nome = jogo.Nome

                    where 

	                    jogo.Id is null

                ")
                .ToListAsync();

        }

    }
}
