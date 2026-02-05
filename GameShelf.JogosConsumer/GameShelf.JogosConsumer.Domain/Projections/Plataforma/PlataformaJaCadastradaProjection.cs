namespace GameShelf.JogosConsumer.Domain.Projections.Plataforma
{
    public class PlataformaJaCadastradaProjection(Guid id, string name)
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
    }
}
