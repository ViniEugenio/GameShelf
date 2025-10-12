using FluentValidation;
using GameShelf.Application.CQRS.Commands.Login;
using GameShelf.Application.CQRS.Validators.ErrorMessages;

namespace GameShelf.Application.CQRS.Validators
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {

        public LoginValidator()
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
