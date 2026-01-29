namespace GameShelf.Domain.Entities
{
    public class Analise : BaseEntity
    {

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid JogoPrateleiraId { get; set; }
        public JogoPrateleira JogoPrateleira { get; set; }

        public string Texto { get; set; }
        public float Classificacao { get; set; }

        public List<Comentario> ComentariosRecebidos { get; set; } = [];

    }
}
