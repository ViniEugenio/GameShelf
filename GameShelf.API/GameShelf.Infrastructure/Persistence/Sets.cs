using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Infrastructure.Persistence
{
    public partial class Context
    {
        public DbSet<User> User { get; set; }
        public DbSet<Jogo> Jogo { get; set; }
        public DbSet<Analise> Analise { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Prateleira> Prateleira { get; set; }
        public DbSet<ParticipantePrateleira> ParticipantePrateleira { get; set; }
        public DbSet<JogoPrateleira> JogoPrateleira { get; set; }
        public DbSet<Outbox> Outbox { get; set; }
        public DbSet<Inbox> Inbox { get; set; }
    }
}
