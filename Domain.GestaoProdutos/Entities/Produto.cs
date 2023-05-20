using System.ComponentModel.DataAnnotations;

namespace Domain.GestaoProdutos.Entities;

public class Produto
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Descricao { get; set; }

    public bool Situacao { get; set; }

    public DateTime? DataFabricacao { get; set; }

    public DateTime? DataValidade { get; set; }
}
