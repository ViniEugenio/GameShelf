using FluentValidation;
using FluentValidation.Results;
using GameShelf.Application.CQRS.Commands.Login;
using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Application.DTOs;

namespace GameShelf.Application.CQRS.Validators
{
    public class LoginValidator
    {

        public static async Task<ResponseDTO> Validar(LoginCommand command)
        {

            ResponseDTO response = new();
            LoginInputValidator validator = new();

            ValidationResult validation = await validator.ValidateAsync(command);

            if (!validation.IsValid)
            {
                response.AdicionarErros(validation);
            }

            return response;

        }

    }

    public class LoginInputValidator : AbstractValidator<LoginCommand>
    {

        public LoginInputValidator()
        {

            RuleFor(login => login.Email)
                .NotEmpty()
                .WithMessage(UsuarioErros.EmailVazio);

            RuleFor(login => login.Password)
                .NotEmpty()
                .WithMessage(UsuarioErros.SenhaVazia);

        }

    }

}
