using FluentValidation;
using FluentValidation.Results;
using GameShelf.Application.CQRS.Commands.CadastrarUsuario;
using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Application.DTOs;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;
using System.Text.RegularExpressions;

namespace GameShelf.Application.CQRS.Validators
{
    public class CadastrarUsuarioValidator(IUsuarioRepository usuarioRepository)
    {

        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<ResponseDTO> Validar(CadastrarUsuarioCommand command)
        {

            ResponseDTO response = new();

            CadastrarUsuarioInputValidator validator = new();
            ValidationResult validation = await validator.ValidateAsync(command);

            if (!validation.IsValid)
            {

                response.AdicionarErros(validation);
                return response;

            }

            if(!await EmailValido(command.Email))
            {
                response.AdicionarErros(UsuarioErros.EmailEmUso);
            }

            return response;

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

    }

    public class CadastrarUsuarioInputValidator : AbstractValidator<CadastrarUsuarioCommand>
    {

        public CadastrarUsuarioInputValidator()
        {

            RuleFor(usuario => usuario.Nome)
                .NotEmpty()
                .WithMessage(UsuarioErros.NomeVazio);

            RuleFor(usuario => usuario.Sobrenome)
                .NotEmpty()
                .WithMessage(UsuarioErros.SobrenomeVazio);

            RuleFor(usuario => usuario.Email)
                .NotEmpty()
                .WithMessage(UsuarioErros.EmailVazio)
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
