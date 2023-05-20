using Application.GestaoProdutos.Services.v1;
using Application.GestaoProdutos.Services.v1.Interfaces;
using Domain.GestaoProdutos.Dtos.ProdutoDtos;
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
        public ActionResult AdicionarProduto([FromBody] Produto produto)
        {
            try
            {
                if (produto is null)
                    return BadRequest("Invalid data");

                var produtoNovo = _produtoService.Create(produto);

                return CreatedAtAction(nameof(ConsultaProdutoId), new { id = produtoNovo.Result.Id }, produtoNovo.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                         ex.Message);
            }
        }

        [HttpGet("/ListarProdutos/nome/{nome}/skip/{skip:int}/take/{take:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<FilterProdutoDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ICollection<FilterProdutoDto>> ListarProdutos([FromRoute] string? nome,[FromRoute] int? skip, [FromRoute] int? take)
        {
            try
            {
                if (skip == null || take == null)
                    return BadRequest($"Configurações gerais não disponíveis, informe todos os parametros");

                var produtoList = _produtoService.FindAll(nome, (int)skip, (int)take);
                if (produtoList == null)
                    return NotFound();

                return Ok(produtoList.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                         ex.Message);
            }
        }

        [HttpGet("/ConsultaProdutoId/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Produto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult ConsultaProdutoId(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest($"Configurações gerais não disponíveis, informe todos os parametros");

                var produto = _produtoService.FindByID(id);
                if (produto == null)
                    return NotFound($"Produto não encontrado...");

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                         ex.Message);
            }
        }

        [HttpPut("/AtualizaProduto")]
        public IActionResult AtualizaProduto([FromBody] Produto produto)
        {
            try
            {
                var produtoUp = _produtoService.Update(produto);
                if (produtoUp == null)
                    return NotFound($"Produto não encontrado...");

                return Ok(produtoUp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                         ex.Message);
            }
        }

        [HttpDelete("/DeletarProduto/{id:int}")]
        public ActionResult DeletarProduto(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest($"Configurações gerais não disponíveis, informe todos os parametros");

                var produto = _produtoService.Disable(id);
                if (produto == null)
                    return NotFound($"Produto não encontrado...");

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                         ex.Message);
            }
        }

    }
}
