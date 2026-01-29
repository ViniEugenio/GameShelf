using FluentValidation;
using GameShelf.Application.CQRS.Queries;
using GameShelf.Application.CQRS.Validators.ErrorMessages;

namespace GameShelf.Application.CQRS.Validators
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
