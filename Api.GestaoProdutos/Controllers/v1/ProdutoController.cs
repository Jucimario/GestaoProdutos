using Domain.GestaoProdutos.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api.GestaoProdutos.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        [HttpPost("AdicionarProduto")]

        public ActionResult AdicionarProduto([FromBody] Produto produto)
        {
            return NoContent();
        }

        [HttpGet("ListarProdutos")]

        public ActionResult<ICollection<Produto>> ListarProdutos()
        {
            return NoContent();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Produto produto)
        {
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            return NoContent();
        }
      
    }
}
