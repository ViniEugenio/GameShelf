namespace GameShelf.JogosConsumer.Domain.Entities;

public class Jogo : BaseEntity
{

    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Imagem { get; set; }

    public List<JogoPlataforma> Plataformas { get; set; } = [];
    public List<JogoGenero> Generos { get; set; } = [];

}
