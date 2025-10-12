using FluentValidation;
using FluentValidation.Results;
using GameShelf.Application.CQRS.Commands.DesativarUsuario;
using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Application.DTOs;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;

namespace GameShelf.Application.CQRS.Validators
{
    public class DesativarUsuarioValidator(IUsuarioRepository usuarioRepository)
    {

        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<ResponseDTO> Validar(DesativarUsuarioCommand command)
        {

            ResponseDTO response = new();

            DesativarUsuarioInputValidator inputValidator = new();
            ValidationResult validation = await inputValidator.ValidateAsync(command);

            if (!validation.IsValid)
            {

                response.AdicionarErros(validation);
                return response;

            }

            if (!await UsuarioValidoParaInativacao(command.Id))
            {
                response.AdicionarErros(UsuarioErros.ErroInativacao);
            }

            return response;

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

    public class DesativarUsuarioInputValidator : AbstractValidator<DesativarUsuarioCommand>
    {

        public DesativarUsuarioInputValidator()
        {

            RuleFor(usuario => usuario.Id)
                .NotEmpty()
                .WithMessage(UsuarioErros.IdVazio);

        }

    }

}
