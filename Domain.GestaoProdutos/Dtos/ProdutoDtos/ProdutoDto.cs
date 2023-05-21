using System.ComponentModel.DataAnnotations;

namespace Domain.GestaoProdutos.Dtos.ProdutoDtos;

public  class ProdutoDto
{
    public string Descricao { get; set; }

    public bool Situacao { get; set; }

    [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
    public DateTime? DataFabricacao { get; set; }
    [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
    public DateTime? DataValidade { get; set; }
}
