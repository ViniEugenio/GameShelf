namespace GameShelf.Domain.Filters
{
    public abstract class PaginatedFilterBase
    {
        public int PaginaAtual { get; set; }
        public int Quantidade { get; set; }
    }
}
