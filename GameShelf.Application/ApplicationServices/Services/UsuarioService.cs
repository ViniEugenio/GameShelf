﻿using FluentValidation.Results;
using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.Commands.AlterarUsuario;
using GameShelf.Application.Commands.CadastrarUsuario;
using GameShelf.Application.Commands.DesativarUsuario;
using GameShelf.Application.Commands.Login;
using GameShelf.Application.DTOs;
using GameShelf.Application.DTOs.UsuarioDTO;
using GameShelf.Application.Extensions.EntitiesExtensions;
using GameShelf.Application.Queries.GetListagemClaimsUsuarios;
using GameShelf.Application.Queries.GetListagemUsuarios;
using GameShelf.Application.Queries.GetUsuario;
using GameShelf.Application.Validators;
using GameShelf.Application.Validators.ErrorMessages;
using GameShelf.Domain.Entities;
using GameShelf.Domain.Enums;
using GameShelf.Domain.Filters.User;
using GameShelf.Domain.Projections;
using GameShelf.Domain.Projections.User;
using GameShelf.Domain.RepositoriesInterfaces;
using GameShelf.Domain.Security;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace GameShelf.Application.ApplicationServices.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthService _authService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IAuthService authService)
        {
            _usuarioRepository = usuarioRepository;
            _authService = authService;
        }

        public async Task<ResponseDTO> CadastrarUsuario(CadastrarUsuarioCommand command)
        {

            ResponseDTO response = new();

            CadastrarUsuarioValidator validator = new(_usuarioRepository);
            ValidationResult validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {

                response.AdicionarErros(validationResult);
                return response;

            }

            User user = new()
            {
                Nome = command.Nome,
                Sobrenome = command.Sobrenome,
                UserName = command.Email,
                Email = command.Email
            };

            IdentityResult result = await _usuarioRepository.CadastrarUsuario(user, command.Senha);

            if (!result.Succeeded)
            {

                response.AdicionarErros(result);
                return response;

            }

            await AplicarClaimsNovoUsuario(user);

            return response;

        }

        public async Task<ResponseDTO> AlterarUsuario(AlterarUsuarioCommand command)
        {

            ResponseDTO response = new();

            AlterarUsuarioValidator validator = new(_usuarioRepository);
            ValidationResult validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {

                response.AdicionarErros(validationResult);
                return response;

            }

            User usuarioParaAlteracao = await _usuarioRepository.GetById(command.Id);
            usuarioParaAlteracao.AtualizarUsuario(command);

            await _usuarioRepository.Update(usuarioParaAlteracao);

            return response;

        }

        public async Task<ResponseDTO> GetUsuarioSimplificado(GetUsuarioQuery query)
        {

            ResponseDTO response = new();

            GetUsuarioSimplificadoValidator validator = new(_usuarioRepository);
            ValidationResult validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {

                response.AdicionarErros(validationResult);
                return response;

            }

            UsuarioSimplificadoProjection projection = await _usuarioRepository.GetUsuarioSimplificado(query.Id);
            UsuarioSimplificadoDTO usuario = projection.Adapt<UsuarioSimplificadoDTO>();

            response.AddData(usuario);

            return response;

        }

        public async Task<ResponseDTO> DesativarUsuario(DesativarUsuarioCommand command)
        {

            ResponseDTO response = new();

            DesativarUsuarioValidator validator = new(_usuarioRepository);
            ValidationResult validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {

                response.AdicionarErros(validationResult);
                return response;

            }

            User usuarioParaDesativacao = await _usuarioRepository.GetById(command.Id);
            usuarioParaDesativacao.DesativarUsuario();

            await _usuarioRepository.Update(usuarioParaDesativacao);

            return response;

        }

        public async Task<ResponseDTO> GetUsuariosPaginados(GetListagemUsuariosQuery query)
        {

            ResponseDTO response = new();

            PaginacaoValidator validator = new();
            ValidationResult validationResult = validator.Validate(query);

            if (!validationResult.IsValid)
            {

                response.AdicionarErros(validationResult);
                return response;

            }

            GetListagemUsuariosFilter filtro = query.Adapt<GetListagemUsuariosFilter>();

            PaginatedProjection<UsuarioPaginacaoProjection> projection = await _usuarioRepository
                .GetUsuarioPaginados(filtro);

            PaginatedResultDTO<UsuarioListagemDTO> paginacao = new()
            {
                PaginaAtual = query.PaginaAtual,
                QuantidadePorPagina = query.Quantidade,
                QuantidadeTotal = projection.QuantidadeTotal,
                Listagem = projection.Listagem.Adapt<List<UsuarioListagemDTO>>()
            };

            response.AddData(paginacao);

            return response;

        }

        public async Task<ResponseDTO> Login(LoginCommand command)
        {

            ResponseDTO response = new();

            LoginValidator validator = new();
            ValidationResult validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
            {

                response.AdicionarErros(validationResult);
                return response;

            }

            SignInResult signInResult = await _usuarioRepository.Login(command.Email, command.Password);

            if (!signInResult.Succeeded)
            {

                response.AdicionarErros(UsuarioErros.LoginInvalido);
                return response;

            }

            LoginProjection loginProjection = await _usuarioRepository.GetInformacoesLoginUsuario(command.Email);
            LoginDTO loginDTO = new()
            {
                JWT = _authService.GerarJWT(loginProjection.Claims),
                Usuario = loginProjection.Usuario.Adapt<UsuarioLoginDTO>()
            };

            response.AddData(loginDTO);

            return response;
        }

        private async Task AplicarClaimsNovoUsuario(User user)
        {

            bool primeiroUsuario = (await _usuarioRepository
                .Count(usuario => usuario.Ativo)) == 1;

            Dictionary<string, EClaimPermissions> claims = [];
            if (primeiroUsuario)
            {

                claims
                    .Add(ClaimsManager.Admin, EClaimPermissions.Create | EClaimPermissions.Read | EClaimPermissions.Update | EClaimPermissions.Delete);

            }

            claims
                .Add(ClaimsManager.User, EClaimPermissions.Create | EClaimPermissions.Read | EClaimPermissions.Update | EClaimPermissions.Delete);

            await _usuarioRepository.AdicionarClaims(user, claims);

        }

        public async Task<ResponseDTO> GetListagemClaimsUsuarios(GetListagemClaimsUsuariosQuery query)
        {

            ResponseDTO response = new();

            PaginacaoValidator validator = new();
            ValidationResult validationResult = validator.Validate(query);

            if (!validationResult.IsValid)
            {

                response.AdicionarErros(validationResult);
                return response;

            }

            GetListagemClaimsUsuariosFilter filtro = query.Adapt<GetListagemClaimsUsuariosFilter>();

            PaginatedProjection<UsuarioClaimsProjection> projection = await _usuarioRepository
                .GetUsuariosClaimsPaginados(filtro);

            List<UsuarioClaimsDTO> data = [..

                projection
                    .Listagem
                    .Select(usuario => new UsuarioClaimsDTO()
                    {
                        Id = usuario.Id,
                        Nome = usuario.Nome,
                        Email = usuario.Email,
                        Permissoes = [..

                            usuario
                            .Claims
                            .Select(claim => new PermissoesDTO(claim))

                        ]
                    })

            ];

            response.AddData(data);

            return response;

        }

    }
}
