using FluentValidation;
using GameShelf.Application.CQRS.Commands.CadastrarUsuario;
using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;
using System.Text.RegularExpressions;

namespace GameShelf.Application.CQRS.Validators
{
    public class CadastrarUsuarioValidator : AbstractValidator<CadastrarUsuarioCommand>
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public CadastrarUsuarioValidator(IUsuarioRepository usuarioRepository)
        {

            _usuarioRepository = usuarioRepository;

            RuleFor(usuario => usuario.Nome)
                .NotEmpty()
                .WithMessage(UsuarioErros.NomeVazio);

            RuleFor(usuario => usuario.Sobrenome)
                .NotEmpty()
                .WithMessage(UsuarioErros.SobrenomeVazio);

            RuleFor(usuario => usuario.Email)
                .NotEmpty()
                .WithMessage(UsuarioErros.EmailVazio)
                .MustAsync(async (email, cancellation) => await EmailValido(email))
                .WithMessage(UsuarioErros.EmailEmUso)
                .EmailAddress()
                .WithMessage(UsuarioErros.EmailEmFormatoInvalido);

            RuleFor(usuario => usuario.Senha)
                .NotEmpty()
                .WithMessage(UsuarioErros.SenhaVazia)
                .Equal(usuario => usuario.ConfirmacaoSenha)
                .WithMessage(UsuarioErros.SenhasDiferentes)
                .Must(SenhaFormatoValido)
                .WithMessage(UsuarioErros.SenhaEmFormatoInvalido);

        }

        private async Task<bool> EmailValido(string email)
        {

            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            return !await _usuarioRepository
                .Exists(usuario => usuario.Email == email);

        }

        private bool SenhaFormatoValido(string senha)
        {

            if (string.IsNullOrEmpty(senha))
            {
                return false;
            }

            Regex regexPadraoSenha = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{6,}$");
            return regexPadraoSenha.IsMatch(senha);

        }

    }
}
