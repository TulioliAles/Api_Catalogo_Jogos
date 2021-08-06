using ApiCatalogoJogos.InputDto;
using ApiCatalogoJogos.Services;
using ApiCatalogoJogos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoService;

        public JogosController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewDto>>> GetListaJogo([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _jogoService.ObterListaJogo(pagina, quantidade);

            if (jogos.Count() == 0)
                return NoContent();

            return Ok(jogos);
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewDto>> GetJogo([FromRoute] Guid idJogo)
        {
            var jogo = await _jogoService.ObterJogo(idJogo);

            if (jogo == null)
                return NoContent();

            return Ok(jogo);
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewDto>> InserirJogo([FromBody]JogoInputDto jogoInputDto)
        {
            try
            {
                var jogo = await _jogoService.InserirJogo(jogoInputDto);

                return Ok(jogo);
            }
            //catch (JogoJaCadastradoException ex)
            catch (Exception ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }

        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoInputDto jogoInputDto)
        {
            try
            {
                await _jogoService.AtualizarJogo(idJogo, jogoInputDto);

                return Ok();
            }
            //catch (JogoNaoCadastradoException ex)
            catch (Exception ex)
            {
                return NotFound("não existe este jogo");
            }
        }

        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarPrecoJogo([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _jogoService.AtualizarPrecoJogo(idJogo, preco);

                return Ok();
            }
            //catch (JogoNaoCadastradoException ex)
            catch (Exception ex)
            {
                return NotFound("não existe este jogo");
            }
        }

        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoService.RemoverJogo(idJogo);

                return Ok();
            }
            //catch (JogoNaoCadastradoException ex)
            catch (Exception ex)
            {
                return NotFound("não existe este jogo");
            }
        }
    }
}
