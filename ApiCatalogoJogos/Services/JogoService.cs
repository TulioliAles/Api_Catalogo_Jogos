using ApiCatalogoJogos.Entities;
using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputDto;
using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<List<JogoViewDto>> ObterListaJogo(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.ObterListaJogo(pagina, quantidade);

            return jogos.Select(jogo => new JogoViewDto
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewDto> ObterJogo(Guid id)
        {
            var jogo = await _jogoRepository.ObterJogo(id);

            if(jogo == null)
            {
                return null;
            }

            return new JogoViewDto
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task<JogoViewDto> InserirJogo(JogoInputDto jogo)
        {
            var entidadeJogo = await _jogoRepository.InserirJogo(jogo.Nome, jogo.Produtora);

            if (entidadeJogo.Count() > 0)
                throw new JogoJaCadastradoException();

            var jogoInserido = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await _jogoRepository.InserirJogo(jogoInserido);

            return new JogoViewDto
            {
                Id = jogoInserido.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task AtualizarJogo(Guid id, JogoInputDto jogo)
        {
            var entidadeJogo = await _jogoRepository.ObterJogo(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;

            await _jogoRepository.AtualizarJogo(entidadeJogo);
        }

        public async Task AtualizarPrecoJogo(Guid id, double preco)
        {
            var entidadeJogo = await _jogoRepository.ObterJogo(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Preco = preco;

            await _jogoRepository.AtualizarJogo(entidadeJogo);
        }

        public async Task RemoverJogo(Guid id)
        {
            var jogo = await _jogoRepository.ObterJogo(id);

            if (jogo == null)
                throw new JogoNaoCadastradoException();

            await _jogoRepository.RemoverJogo(jogo.Id);
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }
    }
}
