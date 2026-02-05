namespace GameShelf.JogosConsumer.Domain.Projections.Genero
{
    public class GeneroJaCadastradoProjection(Guid id, string name)
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
    }
}
