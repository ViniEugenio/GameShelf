namespace GameShelf.Application.DTOs
{
    public class PaginatedResultDTO<T>
    {
        public List<T> Listagem { get; set; } = [];
        public int QuantidadePorPagina { get; set; }
        public int QuantidadeTotal { get; set; }
        public int PaginaAtual { get; set; }
    }
}
