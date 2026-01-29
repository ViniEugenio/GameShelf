using FluentValidation;
using GameShelf.Application.CQRS.Commands.DesativarUsuario;
using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;

namespace GameShelf.Application.CQRS.Validators
{
    public class DesativarUsuarioValidator : AbstractValidator<DesativarUsuarioCommand>
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public DesativarUsuarioValidator(IUsuarioRepository usuarioRepository)
        {

            _usuarioRepository = usuarioRepository;

            _usuarioRepository = usuarioRepository;

            RuleFor(usuario => usuario.Id)
                .NotEmpty()
                .WithMessage(UsuarioErros.IdVazio)
                .MustAsync(async (id, cancellationToken) => await UsuarioValidoParaInativacao(id))
                .WithMessage(UsuarioErros.ErroInativacao);

        }

        public async Task<bool> UsuarioValidoParaInativacao(Guid id)
        {

            return await _usuarioRepository
                .Exists(usuario =>

                    usuario.Id == id
                    && usuario.Ativo

                );

        }

    }
}
