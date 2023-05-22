namespace Domain.GestaoProdutos.Dtos.ProdutoDtos;

public  class ProdutoDto
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public bool Situacao { get; set; }
    public DateTime? DataFabricacao { get; set; }        
    public DateTime? DataValidade { get; set; }
}
