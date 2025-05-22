using FluentValidation;
using GameShelf.Application.Commands.Login;
using GameShelf.Application.Validators.ErrorMessages;

namespace GameShelf.Application.Validators
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
