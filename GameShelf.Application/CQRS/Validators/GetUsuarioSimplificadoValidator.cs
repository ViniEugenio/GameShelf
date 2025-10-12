using FluentValidation;
using FluentValidation.Results;
using GameShelf.Application.CQRS.Queries.GetUsuario;
using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Application.DTOs;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;

namespace GameShelf.Application.CQRS.Validators
{
    public class GetUsuarioSimplificadoValidator(IUsuarioRepository usuarioRepository)
    {

        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<ResponseDTO> Validar(GetUsuarioQuery query)
        {

            ResponseDTO response = new();

            GetUsuarioSimplificadoInputValidator validator = new();
            ValidationResult validation = await validator.ValidateAsync(query);

            if (!validation.IsValid)
            {

                response.AdicionarErros(validation);
                return response;

            }

            if (!await UsuarioEncontrado(query.Id))
            {

                response.AdicionarErros(UsuarioErros.UsuarioNaoEncontrado);
                return response;

            }

            return response;

        }

        public async Task<bool> UsuarioEncontrado(Guid id)
        {
            return await _usuarioRepository.Exists(usuario => usuario.Id == id);
        }

    }

    public class GetUsuarioSimplificadoInputValidator : AbstractValidator<GetUsuarioQuery>
    {

        public GetUsuarioSimplificadoInputValidator()
        {

            RuleFor(usuario => usuario.Id)
                .NotEmpty()
                .WithMessage(UsuarioErros.IdVazio);

        }

    }

}
