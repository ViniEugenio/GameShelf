using FluentValidation.Results;
using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.Commands.CriarPrateleira;
using GameShelf.Application.DTOs;
using GameShelf.Application.Validators;
using GameShelf.Domain.Entities;
using GameShelf.Domain.RepositoriesInterfaces;
using Mapster;

namespace GameShelf.Application.ApplicationServices.Services
{
    public class PrateleiraService : IPrateleiraService
    {

        private readonly IPrateleiraRepository _prateleiraRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISessao _sessao;

        public PrateleiraService(IPrateleiraRepository prateleiraRepository, IUsuarioRepository usuarioRepository, ISessao sessao)
        {
            _prateleiraRepository = prateleiraRepository;
            _usuarioRepository = usuarioRepository;
            _sessao = sessao;
        }

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

            Prateleira novaPrateleira = command
                .Adapt<Prateleira>();

            novaPrateleira.UserId = idUsuarioLogado;

            await _prateleiraRepository
                .Add(novaPrateleira);

            response
                .AddData(novaPrateleira.Adapt<NewRegisterDTO>());

            return response;

        }

        public async Task<bool> VerificarUsuarioEhParticipantePrateleira(Guid prateleiraId)
        {

            Guid usuarioLogado = _sessao
                .GetUsuarioLogado()
                .Id;

            return await _prateleiraRepository.VerificarUsuarioEhParticipantePrateleira(prateleiraId, usuarioLogado);

        }
    }
}
