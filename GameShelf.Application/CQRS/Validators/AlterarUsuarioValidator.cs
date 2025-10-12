using FluentValidation;
using FluentValidation.Results;
using GameShelf.Application.CQRS.Commands.AlterarUsuario;
using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Application.DTOs;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;

namespace GameShelf.Application.CQRS.Validators
{
    public class AlterarUsuarioValidator(IUsuarioRepository usuarioRepository)
    {

        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<ResponseDTO> Validar(AlterarUsuarioCommand command)
        {

            ResponseDTO response = new();

            AlterarUsuarioInputValidator inputValidator = new();

            ValidationResult validation = await inputValidator
                .ValidateAsync(command);

            if (!validation.IsValid)
            {

                response.AdicionarErros(validation);
                return response;

            }

            if (!await EmailValido(command))
            {

                response.AdicionarErros(UsuarioErros.EmailEmUso);
                return response;

            }

            if (!await UsuarioEncontrado(command.Id))
            {
                response.AdicionarErros(UsuarioErros.UsuarioNaoEncontrado);
            }

            return response;

        }

        private async Task<bool> EmailValido(AlterarUsuarioCommand command)
        {

            return !await _usuarioRepository
                .Exists(usuario =>

                    usuario.Email == command.Email
                    && usuario.Id != command.Id

                );

        }

        private async Task<bool> UsuarioEncontrado(Guid id)
        {

            return await _usuarioRepository
                .Exists(usuario => usuario.Id == id);

        }

    }

    public class AlterarUsuarioInputValidator : AbstractValidator<AlterarUsuarioCommand>
    {

        public AlterarUsuarioInputValidator()
        {

            RuleFor(usuario => usuario.Id)
               .NotEmpty()
               .WithMessage(UsuarioErros.IdVazio);

            RuleFor(usuario => usuario.Nome)
               .NotEmpty()
               .WithMessage(UsuarioErros.NomeVazio);

            RuleFor(usuario => usuario.Sobrenome)
                .NotEmpty()
                .WithMessage(UsuarioErros.SobrenomeVazio);

            RuleFor(usuario => usuario.Email)
                .NotEmpty()
                .WithMessage(UsuarioErros.EmailVazio)
                .EmailAddress()
                .WithMessage(UsuarioErros.EmailEmFormatoInvalido);

        }

    }

}
