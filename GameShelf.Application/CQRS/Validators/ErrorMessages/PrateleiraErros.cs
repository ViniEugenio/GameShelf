namespace GameShelf.Application.CQRS.Validators.ErrorMessages
{
    public static class PrateleiraErros
    {
        public readonly static string NomeVazio = "Por favor informe o nome da prateleira";
        public readonly static string NomeDuplicado = "Você já possuí uma prateleira com esse nome";
        public readonly static string ParticipantesNaoEncontrados = "Alguns participantes informados não foram encontrados";
        public readonly static string SemPermissaoPrateleira = "Você não tem permissão para visualizar a prateleira";
    }
}
