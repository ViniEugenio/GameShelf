using FluentValidation;
using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.Commands.CriarPrateleira;
using GameShelf.Application.Validators.ErrorMessages;
using GameShelf.Domain.RepositoriesInterfaces;

namespace GameShelf.Application.Validators
{
    public class CriarPrateleiraValidator : AbstractValidator<CriarPrateleiraCommand>
    {

        private readonly IPrateleiraRepository _prateleiraRepository;
        private readonly ISessao _sessao;

        public CriarPrateleiraValidator(IPrateleiraRepository prateleiraRepository, ISessao sessao)
        {

            _prateleiraRepository = prateleiraRepository;
            _sessao = sessao;

            RuleFor(prateleira => prateleira.Nome)
                .NotEmpty()
                .WithMessage(PrateleiraErros.NomeVazio)
                .MustAsync(async (nome, cancellationToken) => await VerificarPrateleiraDuplicada(nome))
                .WithMessage(PrateleiraErros.NomeDuplicado);



        }

        private async Task<bool> VerificarPrateleiraDuplicada(string nome)
        {

            bool nomeDuplicado = await _prateleiraRepository
                .Exists(prateleira =>

                    prateleira.Nome == nome
                    && prateleira.UserId == _sessao.GetUsuarioLogado().Id

                );

            return !nomeDuplicado;

        }

    }
}
