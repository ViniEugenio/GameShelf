using GameShelf.Application.DTOs;
using GameShelf.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GameShelf.Application.Mappings
{
    public static class CommonMappings
    {

        public static NewRegisterDTO MapNewRegister(BaseEntity entity)
        {

            return new NewRegisterDTO()
            {
                Id = entity.Id
            };

        }

        public static NewRegisterDTO MapNewRegister(IdentityUser<Guid> user)
        {

            return new NewRegisterDTO()
            {
                Id = user.Id
            };

        }

        public static NewRegisterDTO MapNewRegister(Outbox outbox)
        {

            return new NewRegisterDTO()
            {
                Id = outbox.Id
            };

        }

    }
}
