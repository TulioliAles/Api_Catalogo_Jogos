using ApiCatalogoJogos.InputDto;
using ApiCatalogoJogos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<JogoViewDto>>> GetListaJogo()
        {
            return Ok();
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewDto>> GetJogo(Guid idJogo)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewDto>> InserirJogo(JogoInputDto jogo)
        {
            return Ok();
        }

        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo(Guid idJogo, JogoInputDto jogo)
        {
            return Ok();
        }

        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarPrecoJogo(Guid idJogo, double preco)
        {
            return Ok();
        }

        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagarJogo(Guid idJogo)
        {
            return Ok();
        }
    }
}
