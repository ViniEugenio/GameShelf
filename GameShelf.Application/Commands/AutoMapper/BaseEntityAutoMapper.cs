using GameShelf.Domain.Entities;

namespace GameShelf.Application.Commands.AutoMapper
{
    public static class BaseEntityAutoMapper
    {

        public static void DesativarEntidade(this BaseEntity entity)
        {
            entity.DataDesativacao = DateTime.Now;
            entity.DataAlteracao = DateTime.Now;
            entity.Ativo = false;
        }

    }
}
