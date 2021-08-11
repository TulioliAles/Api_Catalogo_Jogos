using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public interface IJogoRepository : IDisposable
    {
        Task<List<Jogo>> ObterListaJogo(int pagina, int quantidade);
        Task<Jogo> ObterJogo(Guid id);
        Task<List<Jogo>> ObterProdutora(string nome, string produtora);
        Task InserirJogo(Jogo jogo);
        Task AtualizarJogo(Jogo jogo);
        Task RemoverJogo(Guid id);
    }
}
