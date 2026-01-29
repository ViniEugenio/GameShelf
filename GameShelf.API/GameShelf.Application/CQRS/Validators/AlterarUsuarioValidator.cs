using FluentValidation;
using GameShelf.Application.CQRS.Commands.AlterarUsuario;
using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;

namespace GameShelf.Application.CQRS.Validators
{
    public class AlterarUsuarioValidator : AbstractValidator<AlterarUsuarioCommand>
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public AlterarUsuarioValidator(IUsuarioRepository usuarioRepository)
        {

            _usuarioRepository = usuarioRepository;

            RuleFor(usuario => usuario.Id)
                .NotEmpty()
                .WithMessage(UsuarioErros.IdVazio)
                .MustAsync(async (id, cancellationToken) => await UsuarioEncontrado(id));

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

            RuleFor(usuario => usuario)
                .MustAsync(async (command, cancellation) => await EmailValido(command))
                .WithMessage(UsuarioErros.EmailEmUso);

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
}
