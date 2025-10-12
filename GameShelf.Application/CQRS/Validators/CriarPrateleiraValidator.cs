using FluentValidation;
using FluentValidation.Results;
using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.CQRS.Commands.CriarPrateleira;
using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Application.DTOs;
using GameShelf.Application.DTOs.UsuarioDTO;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;

namespace GameShelf.Application.CQRS.Validators
{
    public class CriarPrateleiraValidator(IPrateleiraRepository prateleiraRepository, IUsuarioRepository usuarioRepository, ISessao sessao)
    {

        private readonly IPrateleiraRepository _prateleiraRepository = prateleiraRepository;
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly ISessao _sessao = sessao;

        public async Task<ResponseDTO> Validar(CriarPrateleiraCommand command)
        {

            ResponseDTO response = new();

            CriarPrateleiraInputValidator validator = new();
            ValidationResult validation = await validator.ValidateAsync(command);

            if (!validation.IsValid)
            {
                response.AdicionarErros(validation);
                return response;
            }

            if (!await VerificarPrateleiraDuplicada(command.Nome))
            {

                response.AdicionarErros(PrateleiraErros.NomeDuplicado);
                return response;

            }

            if (!await VerificarParticipantesPrateleira(command.Participantes))
            {
                response.AdicionarErros(PrateleiraErros.ParticipantesNaoEncontrados);
            }

            return response;

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

    public class CriarPrateleiraInputValidator : AbstractValidator<CriarPrateleiraCommand>
    {

        public CriarPrateleiraInputValidator()
        {

            RuleFor(prateleira => prateleira.Nome)
                 .NotEmpty()
                 .WithMessage(PrateleiraErros.NomeVazio)
                 .WithMessage(PrateleiraErros.NomeDuplicado);

        }

    }

}
