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

        public Task AtualizarJogo(Guid id, JogoInputDto jogo)
        {
            throw new NotImplementedException();
        }


        public Task AtualizarPrecoJogo(Guid id, double preco)
        {
            throw new NotImplementedException();
        }


        public Task RemoverJogo(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
