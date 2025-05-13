using FluentValidation.Results;
using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.Commands.AlterarUsuario;
using GameShelf.Application.Commands.AutoMapper;
using GameShelf.Application.Commands.CadastrarUsuario;
using GameShelf.Application.Commands.DesativarUsuario;
using GameShelf.Application.DTOs;
using GameShelf.Application.DTOs.UsuarioDTO;
using GameShelf.Application.Queries.GetListagemUsuarios;
using GameShelf.Application.Queries.GetUsuario;
using GameShelf.Application.Validators;
using GameShelf.Domain.Entities;
using GameShelf.Domain.RepositoriesInterfaces;
using Microsoft.AspNetCore.Identity;

namespace GameShelf.Application.ApplicationServices.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
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
            response.AdicionarErros(result);

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

            UsuarioSimplificadoDTO usuario = await _usuarioRepository.GetUsuarioSimplificado<UsuarioSimplificadoDTO>(query.Id);
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

            PaginacaoValidator validator = new PaginacaoValidator();
            ValidationResult validationResult = validator.Validate(query);

            if (!validationResult.IsValid)
            {

                response.AdicionarErros(validationResult);
                return response;

            }

            PaginatedResultDTO<UsuarioListagemDTO> paginacao = await _usuarioRepository
                .GetUsuarioPaginados<PaginatedResultDTO<UsuarioListagemDTO>, GetListagemUsuariosQuery>(query);

            response.AddData(paginacao);

            return response;

        }
    }
}
