using GameShelf.Domain.Entities;

namespace GameShelf.Application.Extensions.EntitiesExtensions
{
    public static class BaseEntityExtensions
    {

        public static void DesativarEntidade(this BaseEntity entity)
        {
            entity.DataDesativacao = DateTime.Now;
            entity.DataAlteracao = DateTime.Now;
            entity.Ativo = false;
        }

    }
}
