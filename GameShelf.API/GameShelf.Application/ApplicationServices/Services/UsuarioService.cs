using FluentValidation.Results;
using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.CQRS.Commands.AlterarUsuario;
using GameShelf.Application.CQRS.Commands.CadastrarUsuario;
using GameShelf.Application.CQRS.Commands.DesativarUsuario;
using GameShelf.Application.CQRS.Commands.Login;
using GameShelf.Application.CQRS.Queries.GetListagemClaimsUsuarios;
using GameShelf.Application.CQRS.Queries.GetListagemUsuarios;
using GameShelf.Application.CQRS.Queries.GetUsuario;
using GameShelf.Application.CQRS.Validators;
using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Application.DTOs;
using GameShelf.Application.DTOs.UsuarioDTO;
using GameShelf.Application.Extensions.EntitiesExtensions;
using GameShelf.Application.Mappings;
using GameShelf.Domain.Entities;
using GameShelf.Domain.Enums;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;
using GameShelf.Domain.Models.Filters.User;
using GameShelf.Domain.Models.Projections;
using GameShelf.Domain.Models.Projections.User;
using GameShelf.Domain.Models.Security;
using Microsoft.AspNetCore.Identity;

namespace GameShelf.Application.ApplicationServices.Services
{
    public class UsuarioService(IUsuarioRepository usuarioRepository, IAuthService authService, ISessao sessao) : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IAuthService _authService = authService;
        private readonly ISessao _sessao = sessao;

        public async Task<ResponseDTO> CadastrarUsuario(CadastrarUsuarioCommand command)
        {

            ResponseDTO response = new();

            CadastrarUsuarioValidator validator = new(_usuarioRepository);

            ValidationResult validationResult = await validator
                .ValidateAsync(command);

            if (!validationResult.IsValid)
            {

                response.AdicionarErros(validationResult);
                return response;

            }

            User user = UsuarioMappings
                .MapNovoUsuario(command);

            IdentityResult result = await _usuarioRepository
                .CadastrarUsuario(user, command.Senha);

            if (!result.Succeeded)
            {

                response
                    .AdicionarErros(result);

                return response;

            }

            await AplicarClaimsPrimeiroUsuario(user);

            response
                .AddData(CommonMappings.MapNewRegister(user));

            return response;

        }

        public async Task<ResponseDTO> AlterarUsuario(AlterarUsuarioCommand command)
        {

            ResponseDTO response = new();

            AlterarUsuarioValidator validator = new(_usuarioRepository);

            ValidationResult validationResult = await validator
                .ValidateAsync(command);

            if (!validationResult.IsValid)
            {

                response
                    .AdicionarErros(validationResult);

                return response;

            }

            User usuarioParaAlteracao = await _usuarioRepository
                .GetById(command.Id);

            usuarioParaAlteracao
                .AtualizarUsuario(command);

            await _usuarioRepository
                .Update(usuarioParaAlteracao);

            return response;

        }

        public async Task<ResponseDTO> GetUsuarioSimplificado(GetUsuarioQuery query)
        {

            ResponseDTO response = new();

            GetUsuarioSimplificadoValidator validator = new(_usuarioRepository);

            ValidationResult validationResult = await validator
                .ValidateAsync(query);

            if (!validationResult.IsValid)
            {

                response.AdicionarErros(validationResult);
                return response;

            }

            UsuarioSimplificadoProjection projection = await _usuarioRepository
                .GetUsuarioSimplificado(query.Id);

            UsuarioSimplificadoDTO usuario = UsuarioMappings
                .MapUsuarioSimplificado(projection);

            response
                .AddData(usuario);

            return response;

        }

        public async Task<ResponseDTO> DesativarUsuario(DesativarUsuarioCommand command)
        {

            ResponseDTO response = new();

            DesativarUsuarioValidator validator = new(_usuarioRepository);

            ValidationResult validationResult = await validator
                .ValidateAsync(command);

            if (!validationResult.IsValid)
            {

                response
                    .AdicionarErros(validationResult);

                return response;

            }

            User usuarioParaDesativacao = await _usuarioRepository
                .GetById(command.Id);

            usuarioParaDesativacao
                .DesativarUsuario();

            await _usuarioRepository
                .Update(usuarioParaDesativacao);

            return response;

        }

        public async Task<ResponseDTO> GetUsuariosPaginados(GetListagemUsuariosQuery query)
        {

            ResponseDTO response = new();

            PaginacaoValidator validator = new();

            ValidationResult validationResult = validator
                .Validate(query);

            if (!validationResult.IsValid)
            {

                response.AdicionarErros(validationResult);
                return response;

            }

            GetListagemUsuariosFilter filtro = UsuarioMappings
                .MapFiltroListagemUsuarios(query);

            PaginatedProjection<UsuarioPaginacaoProjection> projection = await _usuarioRepository
                .GetUsuarioPaginados(filtro);

            PaginatedResultDTO<UsuarioListagemDTO> paginacao = UsuarioMappings
                .MapListagemsUsuarios(projection, query);

            response
                .AddData(paginacao);

            return response;

        }

        public async Task<ResponseDTO> Login(LoginCommand command)
        {

            ResponseDTO response = new();

            LoginValidator validator = new();

            ValidationResult validationResult = validator
                .Validate(command);

            if (!validationResult.IsValid)
            {

                response
                    .AdicionarErros(validationResult);

                return response;

            }

            SignInResult signInResult = await _usuarioRepository
                .Login(command.Email, command.Password);

            if (!signInResult.Succeeded)
            {

                response
                    .AdicionarErros(UsuarioErros.LoginInvalido);

                return response;

            }

            LoginProjection loginProjection = await _usuarioRepository
                .GetInformacoesLoginUsuario(command.Email);

            string jwt = _authService.GerarJWT(loginProjection.Claims);

            LoginDTO loginDTO = UsuarioMappings
                .MapLoginUsuario(loginProjection, jwt);

            _sessao
                .SetUsuarioLogado(loginDTO.Usuario);

            response
                .AddData(loginDTO);

            return response;

        }

        private async Task AplicarClaimsPrimeiroUsuario(User user)
        {

            bool primeiroUsuario = (await _usuarioRepository
                .Count(usuario => usuario.Ativo)) == 1;

            if (!primeiroUsuario)
            {
                return;
            }

            Dictionary<string, EClaimPermissions> claims = [];

            claims
                .Add(

                    ClaimsManager.Admin, EClaimPermissions.Create
                    | EClaimPermissions.Read
                    | EClaimPermissions.Update
                    | EClaimPermissions.Delete

                );

            await _usuarioRepository.AdicionarClaims(user, claims);

        }

        public async Task<ResponseDTO> GetListagemClaimsUsuarios(GetListagemClaimsUsuariosQuery query)
        {

            ResponseDTO response = new();

            PaginacaoValidator validator = new();

            ValidationResult validationResult = validator
                .Validate(query);

            if (!validationResult.IsValid)
            {

                response
                    .AdicionarErros(validationResult);

                return response;

            }

            GetListagemClaimsUsuariosFilter filtro = UsuarioMappings
                .MapFiltroListagemClaimsUsuarios(query);

            PaginatedProjection<UsuarioClaimsProjection> projection = await _usuarioRepository
                .GetUsuariosClaimsPaginados(filtro);

            response
                .AddData(UsuarioMappings.MapListagemClaimsUsuarios(projection));

            return response;

        }

    }
}
