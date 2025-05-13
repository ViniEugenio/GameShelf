using FluentValidation;
using GameShelf.Application.Commands.DesativarUsuario;
using GameShelf.Application.Validators.ErrorMessages;
using GameShelf.Domain.RepositoriesInterfaces;

namespace GameShelf.Application.Validators
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
