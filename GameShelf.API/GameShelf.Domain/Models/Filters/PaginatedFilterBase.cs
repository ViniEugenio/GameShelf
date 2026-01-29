namespace GameShelf.Domain.Models.Filters
{
    public abstract class PaginatedFilterBase
    {
        public int PaginaAtual { get; set; }
        public int Quantidade { get; set; }
    }
}
