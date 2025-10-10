using GameShelf.Domain.Entities;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;
using GameShelf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Infrastructure.Repositories
{
    public class PrateleiraRepository(Context context) : BaseRepository<Prateleira>(context), IPrateleiraRepository
    {
        public async Task<bool> VerificarUsuarioEhParticipantePrateleira(Guid prateleiraId, Guid usuarioLogadoId)
        {

            return await _dbSet
                .AsNoTracking()
                .GroupJoin(

                    _context
                        .ParticipantePrateleira
                        .AsNoTracking(),

                    prateleira => prateleira.Id,
                    participante => participante.PrateleiraId,
                    (prateleira, participante) => new
                    {
                        prateleira,
                        participante
                    }

                )
                .SelectMany(

                   join => join.participante.DefaultIfEmpty(),
                   (join, participante) => new
                   {
                       PrateleiraId = join.prateleira.Id,
                       DonoPrateleiraId = join.prateleira.UserId,
                       ParticipanteId = participante.UserId,
                       ParticipanteAtivo = participante.Ativo
                   }

                )
                .AnyAsync(join =>

                   join.PrateleiraId == prateleiraId
                   && (

                       join.DonoPrateleiraId == usuarioLogadoId
                       || (

                           join.ParticipanteId == usuarioLogadoId
                           && join.ParticipanteAtivo

                       )

                   )

                );

        }

    }
}
