using FluentValidation;
using GameShelf.Application.CQRS.Queries.GetUsuario;
using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;

namespace GameShelf.Application.CQRS.Validators
{
    public class GetUsuarioSimplificadoValidator : AbstractValidator<GetUsuarioQuery>
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public GetUsuarioSimplificadoValidator(IUsuarioRepository usuarioRepository)
        {

            _usuarioRepository = usuarioRepository;

            RuleFor(usuario => usuario.Id)
                .NotEmpty()
                .WithMessage(UsuarioErros.IdVazio)
                .MustAsync(async (id, cancellationToken) => await UsuarioEncontrado(id))
                .WithMessage(UsuarioErros.UsuarioNaoEncontrado);

        }

        public async Task<bool> UsuarioEncontrado(Guid id)
        {
            return await _usuarioRepository.Exists(usuario => usuario.Id == id);
        }

    }
}
