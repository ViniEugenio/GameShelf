using GameShelf.JogosConsumer.Application.DTOs.Jogos;

namespace GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces
{
    public interface IPlataformaService
    {
        Task<List<AtualizarJogosAuxiliarDTO>> CadastrarNovasPlataformas(List<string> plataformas);
        Task<List<string>> FiltrarPlataformasAindaNaoCadastradas(List<string> plataformas);
        Task<List<AtualizarJogosAuxiliarDTO>> GetPlataformasFiltradasPorNome(List<string> plataformas);
    }
}
