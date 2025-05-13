namespace GameShelf.Domain.Entities
{
    public class Jogo : BaseEntity
    {

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }

        public List<Analise> AnalisesRecebidas { get; set; } = [];

    }
}
