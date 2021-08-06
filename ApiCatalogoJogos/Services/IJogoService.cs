using ApiCatalogoJogos.InputDto;
using ApiCatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Services
{
    public interface IJogoService
    {
        Task<List<JogoViewDto>> ObterListaJogo(int pagina, int quantidade);
        Task<JogoViewDto> ObterJogo(Guid id);
        Task<JogoViewDto> InserirJogo(JogoInputDto jogo);
        Task AtualizarJogo(Guid id, JogoInputDto jogo);
        Task AtualizarPrecoJogo(Guid id, double preco);
        Task RemoverJogo(Guid id);
    }
}
