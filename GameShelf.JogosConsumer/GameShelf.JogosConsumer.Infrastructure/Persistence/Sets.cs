using GameShelf.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.JogosConsumer.Infrastructure.Persistence
{
    public partial class Context
    {
        public DbSet<Jogo> Jogo { get; set; }
        public DbSet<Plataforma> Plataforma { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Inbox> Inbox { get; set; }
    }
}
