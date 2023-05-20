using Domain.GestaoProdutos.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.GestaoProdutos.Entities;

public class Produto : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Descricao { get; set; }

    public bool Situacao { get; set; }

    
    public DateTime? DataFabricacao { get; set; }
    

    public DateTime? DataValidade { get; set; }
}
