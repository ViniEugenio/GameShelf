namespace GameShelf.Domain.Projections
{
    public abstract class PaginatedFilterBaseProjection
    {
        public int PaginaAtual { get; set; }
        public int Quantidade { get; set; }
    }
}
