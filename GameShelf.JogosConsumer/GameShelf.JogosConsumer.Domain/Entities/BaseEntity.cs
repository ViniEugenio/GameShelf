namespace GameShelf.JogosConsumer.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DataAtivacao { get; set; } = DateTime.Now;
        public DateTime? DataDesativacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
