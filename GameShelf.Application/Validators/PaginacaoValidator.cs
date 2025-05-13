using FluentValidation;
using GameShelf.Application.Queries;
using GameShelf.Application.Validators.ErrorMessages;

namespace GameShelf.Application.Validators
{
    public class PaginacaoValidator : AbstractValidator<PaginatedQueryBase>
    {

        public PaginacaoValidator()
        {

            RuleFor(paginacao => paginacao.Quantidade)
                .GreaterThan(0)
                .WithMessage(BaseErros.PaginacaoQuantidadeZero);

            RuleFor(paginacao => paginacao.PaginaAtual)
                .GreaterThan(0)
                .WithMessage(BaseErros.PaginacaoPaginaAtualNegativa);

        }

    }
}
