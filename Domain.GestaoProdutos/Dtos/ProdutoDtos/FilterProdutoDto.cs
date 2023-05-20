namespace Domain.GestaoProdutos.Dtos.ProdutoDtos;

public class FilterProdutoDto
{
    public int TotalRegistro { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public int PaginaAtual { get; set; }
    public int TotalPagina { get; set; }

    public ICollection<ProdutoDto>? Produtos { get; set; }
   
}
