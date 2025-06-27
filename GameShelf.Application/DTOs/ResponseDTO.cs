using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace GameShelf.Application.DTOs
{
    public class ResponseDTO
    {

        public string Response { get; private set; } = "Requisição efetuada com sucesso!";
        public List<string> Erros { get; private set; } = [];
        public object Data { get; private set; }

        public void AdicionarErros(params string[] erros)
        {

            Response = "Não foi possível processar a sua requisição, alguns erros foram identificados";
            Erros.AddRange(erros);

        }

        public void AdicionarErros(ValidationResult validationResult)
        {

            string[] erros = [.. validationResult
                .Errors
                .Select(erro => erro.ErrorMessage)];

            AdicionarErros(erros);

        }

        public void AdicionarErros(IdentityResult identityResult)
        {

            string[] erros = [.. identityResult
                .Errors
                .Select(erro => erro.Description)];

            AdicionarErros(erros);

        }

        public bool IsValid()
        {
            return Erros.Count == 0;
        }

        public void AddData(object data)
        {
            Data = data;
        }

    }
}
