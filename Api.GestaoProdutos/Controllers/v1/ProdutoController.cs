using Application.GestaoProdutos.Services.v1;
using Application.GestaoProdutos.Services.v1.Interfaces;
using Domain.GestaoProdutos.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api.GestaoProdutos.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService ??
                    throw new ArgumentNullException(nameof(produtoService));
        }


        [HttpPost("/AdicionarProduto")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Produto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult AdicionarProduto([FromBody] Produto produto)
        {
            if (produto is null)
                return BadRequest("Invalid data");

            var produtoNovo = _produtoService.Create(produto);

            return CreatedAtAction(nameof(ConsultaProdutoId), new { id = produtoNovo.Id }, produtoNovo);
        }

        [HttpGet("/ListarProdutos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Produto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ICollection<Produto>> ListarProdutos([FromQuery] int? skip = 0, [FromQuery] int? take = 50)
        {
            try
            {
                if (skip == null || take == null)
                    return BadRequest($"Configurações gerais não disponíveis, informe todos os parametros");


                var produtoList = _produtoService.FindAll((int)skip, (int)take);

                if (produtoList == null)
                    return NotFound();

                return Ok(produtoList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                         "Houve um problema no gerenciamento do seu pedido.");
            }
        }

        [HttpGet("/ConsultaProdutoId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Produto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult ConsultaProdutoId(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest($"Configurações gerais não disponíveis, informe todos os parametros");
                }

                var produto = _produtoService.FindByID(id);

                if (produto == null)
                    return NotFound($"Produto não encontrado...");

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                         "Houve um problema no gerenciamento do seu pedido.");
            }
        }

        [HttpPut("/AtualizaProduto")]
        public IActionResult AtualizaProduto([FromBody] Produto produto)
        {
            try
            {
                var produtoUp = _produtoService.Update(produto);
                if (produtoUp == null)
                {
                    return NotFound($"Produto não encontrado...");
                }

                return Ok(produtoUp);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                         "Houve um problema no gerenciamento do seu pedido.");
            }
        }

        [HttpDelete("/DeletarProduto/{id}")]
        public ActionResult DeletarProduto(int id)
        {
            try
            {
                var produto = _produtoService.Disable(id);
                if (produto == null)
                {
                    return NotFound($"Produto não encontrado...");
                }

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                         "Houve um problema no gerenciamento do seu pedido.");
            }
        }

    }
}
