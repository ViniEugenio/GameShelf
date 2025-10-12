using FluentValidation;
using FluentValidation.Results;
using GameShelf.Application.CQRS.Queries;
using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Application.DTOs;

namespace GameShelf.Application.CQRS.Validators
{
    public class PaginacaoValidator
    {
        
        public static async Task<ResponseDTO> Validar(PaginatedQueryBase query)
        {

            ResponseDTO response = new();
            PaginacaoInputValidator validator = new();

            ValidationResult validation = await validator.ValidateAsync(query);

            if (!validation.IsValid)
            {
                response.AdicionarErros(validation);
            }

            return response;

        }

    }

    public class PaginacaoInputValidator : AbstractValidator<PaginatedQueryBase>
    {

        public PaginacaoInputValidator()
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
