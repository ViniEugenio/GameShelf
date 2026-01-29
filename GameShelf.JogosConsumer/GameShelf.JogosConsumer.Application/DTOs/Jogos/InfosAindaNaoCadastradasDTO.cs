using GameShelf.JogosConsumer.Domain.Projections.RawG;

namespace GameShelf.JogosConsumer.Application.DTOs.Jogos
{
    public class InfosAindaNaoCadastradasDTO
    {

        public List<RawGGameProjection> ResultadoPagina { get; set; } = [];
        public List<string> JogosAindaNaoCadastrados { get; set; } = [];
        public List<string> GenerosAindaNaoCadastrados { get; set; } = [];
        public List<string> PlataformasAindaNaoCadastradas { get; set; } = [];

        public InfosAindaNaoCadastradasDTO()
        {

        }

        public InfosAindaNaoCadastradasDTO(List<RawGGameProjection> resultadoPagina, List<string> jogosAindaNaoCadastrados, List<string> generosAindaNaoCadastrados, List<string> plataformasAindaNaoCadastradas)
        {
            ResultadoPagina = resultadoPagina;
            JogosAindaNaoCadastrados = jogosAindaNaoCadastrados;
            GenerosAindaNaoCadastrados = generosAindaNaoCadastrados;
            PlataformasAindaNaoCadastradas = plataformasAindaNaoCadastradas;
        }

    }
}
