using FluentValidation;
using GameShelf.Application.Queries.GetUsuario;
using GameShelf.Application.Validators.ErrorMessages;
using GameShelf.Domain.RepositoriesInterfaces;

namespace GameShelf.Application.Validators
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
