namespace GameShelf.Application.CQRS.Validators.ErrorMessages
{
    public static class BaseErros
    {
        public static readonly string PaginacaoQuantidadeZero = "A quantidade de registros da paginação deve ser maior que zero";
        public static readonly string PaginacaoPaginaAtualNegativa = "A página deve ser maior que zero";
    }
}
