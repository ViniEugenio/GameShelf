using FluentValidation.Results;
using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.CQRS.Commands.CriarPrateleira;
using GameShelf.Application.CQRS.Validators;
using GameShelf.Application.DTOs;
using GameShelf.Application.Mappings;
using GameShelf.Domain.Entities;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;

namespace GameShelf.Application.ApplicationServices.Services
{
    public class PrateleiraService(IPrateleiraRepository prateleiraRepository, IUsuarioRepository usuarioRepository, ISessao sessao) : IPrateleiraService
    {

        private readonly IPrateleiraRepository _prateleiraRepository = prateleiraRepository;
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly ISessao _sessao = sessao;

        public async Task<ResponseDTO> CriarPrateleira(CriarPrateleiraCommand command)
        {

            Guid idUsuarioLogado = _sessao
                .GetUsuarioLogado()
                .Id;

            command
                .Participantes
                .Remove(idUsuarioLogado);

            ResponseDTO response = new();

            CriarPrateleiraValidator validator = new(_prateleiraRepository, _usuarioRepository, _sessao);

            ValidationResult validacao = await validator
                .ValidateAsync(command);

            if (!validacao.IsValid)
            {

                response
                    .AdicionarErros(validacao);

                return response;

            }

            Prateleira novaPrateleira = PrateleiraMappings.MapNovaPrateleira(command, _sessao);

            await _prateleiraRepository
                .Add(novaPrateleira);

            response
                .AddData(CommonMappings.MapNewRegister(novaPrateleira));

            return response;

        }

        public async Task<bool> VerificarUsuarioEhParticipantePrateleira(Guid prateleiraId)
        {

            Guid usuarioLogado = _sessao
                .GetUsuarioLogado()
                .Id;

            return await _prateleiraRepository
                .VerificarUsuarioEhParticipantePrateleira(prateleiraId, usuarioLogado);

        }
    }
}
