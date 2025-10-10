namespace GameShelf.Application.CQRS.Validators.ErrorMessages
{
    public static class UsuarioErros
    {
        public readonly static string NomeVazio = "Por favor informe o seu nome";
        public readonly static string SobrenomeVazio = "Por favor informe o seu sobrenome";
        public readonly static string EmailVazio = "Por favor informe o seu email";
        public readonly static string EmailEmUso = "Esse email já está sendo usado por outro usuário";
        public readonly static string EmailEmFormatoInvalido = "O email informado não está em formato válido";
        public readonly static string SenhaVazia = "Por favor informe sua senha";
        public readonly static string SenhasDiferentes = "As senha informadas não são iguais";
        public readonly static string SenhaEmFormatoInvalido = "Sua senha deve conter pelo menos 1 letra maiúscula, 1 número, 1 caracter especial e ao menos 6 dígitos";
        public readonly static string IdVazio = "Por favor informe o id do usuário";
        public readonly static string UsuarioNaoEncontrado = "O usuário informado não foi encontrado";
        public readonly static string ErroInativacao = "Não foi possível desativar esse usuário, ele não foi encontrado ou já está inativo";
        public readonly static string LoginInvalido = "Email e/ou senha inválidos";
        public readonly static string UsuarioNaoLogado = "Por favor faça seu login e tente novamente!";
        public readonly static string AcessoNegado = "Você não possuí permissao para realizar essa ação!";
    }
}
