using GameShelf.JogosConsumer.Application.DTOs.Jogos;

namespace GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces
{
    public interface IGeneroService
    {
        Task<List<string>> FiltrarGenerosAindaNaoCadastrados(List<string> generos);
        Task<List<AtualizarJogosAuxiliarDTO>> CadastrarNovosGeneros(List<string> generos);
        Task<List<AtualizarJogosAuxiliarDTO>> GetGenerosFiltradosPorNome(List<string> generos);
    }
}
