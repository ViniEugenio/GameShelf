using Microsoft.AspNetCore.Identity;

namespace GameShelf.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataAtivacao { get; set; } = DateTime.Now;
        public DateTime? DataDesativacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Ativo { get; set; } = true;

        public List<Analise> AnalisesFeitas { get; set; } = [];
        public List<Comentario> ComentariosFeitos { get; set; } = [];
        public List<Prateleira> MinhasPrateleiras { get; set; } = [];
        public List<ParticipantePrateleira> PrateleirasParticipante { get; set; } = [];
        public List<JogoPrateleira> JogosInseridosEmPrateleiras { get; set; } = [];

    }
}
