namespace GameShelf.Domain.Entities
{
    public class Analise : BaseEntity
    {

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid JogoId { get; set; }
        public Jogo Jogo { get; set; }

        public string Texto { get; set; }
        public float Classificacao { get; set; }

        public List<Comentario> ComentariosRecebidos { get; set; } = [];

    }
}
