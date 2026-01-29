namespace GameShelf.Domain.Models.Projections
{
    public class PaginatedProjection<T>
    {
        public int QuantidadeTotal { get; set; }
        public List<T> Listagem { get; set; }

        public PaginatedProjection(int quantidadeTotal, List<T> listagem)
        {
            QuantidadeTotal = quantidadeTotal;
            Listagem = listagem;
        }
    }
}
