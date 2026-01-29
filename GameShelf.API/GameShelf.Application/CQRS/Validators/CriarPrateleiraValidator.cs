using FluentValidation;
using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.CQRS.Commands.CriarPrateleira;
using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Application.DTOs.UsuarioDTO;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;

namespace GameShelf.Application.CQRS.Validators
{
    public class CriarPrateleiraValidator : AbstractValidator<CriarPrateleiraCommand>
    {

        private readonly IPrateleiraRepository _prateleiraRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISessao _sessao;

        public CriarPrateleiraValidator(IPrateleiraRepository prateleiraRepository, IUsuarioRepository usuarioRepository, ISessao sessao)
        {

            _prateleiraRepository = prateleiraRepository;
            _usuarioRepository = usuarioRepository;
            _sessao = sessao;

            RuleFor(prateleira => prateleira.Nome)
                .NotEmpty()
                .WithMessage(PrateleiraErros.NomeVazio)
                .MustAsync(async (nome, cancellationToken) => await VerificarPrateleiraDuplicada(nome))
                .WithMessage(PrateleiraErros.NomeDuplicado);

            RuleFor(prateleira => prateleira.Participantes)
                .MustAsync(async (participantes, cancellationToken) => await VerificarParticipantesPrateleira(participantes))
                .WithMessage(PrateleiraErros.ParticipantesNaoEncontrados);

        }

        private async Task<bool> VerificarPrateleiraDuplicada(string nome)
        {

            UsuarioLoginDTO usuarioLogado = _sessao.GetUsuarioLogado();

            bool nomeDuplicado = await _prateleiraRepository
                .Exists(prateleira =>

                    prateleira.Nome == nome
                    && prateleira.UserId == usuarioLogado.Id

                );

            return !nomeDuplicado;

        }

        private async Task<bool> VerificarParticipantesPrateleira(List<Guid> participantes)
        {

            if (participantes.Count == 0)
            {
                return true;
            }

            int quantidadeParticipantesEncontrados = await _usuarioRepository
                .Count(usuario => participantes.Contains(usuario.Id));

            return quantidadeParticipantesEncontrados == participantes.Count;

        }

    }
}
