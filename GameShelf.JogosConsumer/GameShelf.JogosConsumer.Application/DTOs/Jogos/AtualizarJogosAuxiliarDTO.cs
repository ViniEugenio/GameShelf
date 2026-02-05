namespace GameShelf.JogosConsumer.Application.DTOs.Jogos
{
    public class AtualizarJogosAuxiliarDTO(Guid id, string name)
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
    }
}
