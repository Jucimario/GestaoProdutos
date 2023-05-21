using Application.GestaoProdutos.Services.v1.Interfaces;
using Domain.GestaoProdutos.Dtos.ProdutoDtos;
using Domain.GestaoProdutos.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.GestaoProdutos.Controllers.v1;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    public async Task<ActionResult> AdicionarProduto([FromBody] Produto produto)
    {
        try
        {
            if (produto is null)
                return BadRequest("Invalid data");

            var produtoNovo = await _produtoService.Create(produto);

            return CreatedAtAction(nameof(ConsultaProdutoId), new { id = produtoNovo.Id }, produtoNovo);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest,
                     ex.Message);
        }
    }

    [HttpGet("/ListarProdutos/skip/{skip:int}/take/{take:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<FilterProdutoDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task< ActionResult<ICollection<FilterProdutoDto>>> ListarProdutos([FromQuery] string? nome,[FromRoute] int? skip, [FromRoute] int? take)
    {
        try
        {
            if (skip == null || take == null)
                return BadRequest($"Configurações gerais não disponíveis, informe todos os parametros");

            FilterProdutoDto produtoList = await _produtoService.FindAll(nome, (int)skip, (int)take);
            if (produtoList == null)
                return NotFound();

            return Ok(produtoList);
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
    public async Task<ActionResult> ConsultaProdutoId(int id)
    {
        try
        {
            if (id <= 0)
                return BadRequest($"Configurações gerais não disponíveis, informe todos os parametros");

            var produto =  await _produtoService.FindByID(id);
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
    public async Task<ActionResult> AtualizaProduto([FromBody] Produto produto)
    {
        try
        {
            var produtoUp = await _produtoService.Update(produto);
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
    public async Task<ActionResult> DeletarProduto(int id)
    {
        try
        {
            if (id <= 0)
                return BadRequest($"Configurações gerais não disponíveis, informe todos os parametros");

            var produto = await _produtoService.Disable(id);
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
