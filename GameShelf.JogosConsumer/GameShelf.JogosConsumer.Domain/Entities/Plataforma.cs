namespace GameShelf.JogosConsumer.Domain.Entities;

public partial class Plataforma : BaseEntity
{

    public string Nome { get; set; }

    public List<JogoPlataforma> Jogos { get; set; } = [];

}
