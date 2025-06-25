using FluentValidation.Results;
using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.Commands.CriarPrateleira;
using GameShelf.Application.DTOs;
using GameShelf.Application.Validators;
using GameShelf.Domain.RepositoriesInterfaces;

namespace GameShelf.Application.ApplicationServices.Services
{
    public class PrateleiraService : IPrateleiraService
    {

        private readonly IPrateleiraRepository _prateleiraRepository;
        private readonly ISessao _sessao;

        public PrateleiraService(IPrateleiraRepository prateleiraRepository, ISessao sessao)
        {
            _prateleiraRepository = prateleiraRepository;
            _sessao = sessao;
        }

        public async Task<ResponseDTO> CriarPrateleira(CriarPrateleiraCommand command)
        {

            ResponseDTO response = new();

            CriarPrateleiraValidator validator = new(_prateleiraRepository, _sessao);
            ValidationResult validacao = await validator.ValidateAsync(command);

            if (!validacao.IsValid)
            {

                response.AdicionarErros(validacao);
                return response;

            }

            return response;

        }

    }
}
