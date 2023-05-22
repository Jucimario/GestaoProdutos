using Domain.GestaoProdutos.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.GestaoProdutos.Entities;

public class Produto : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Descricao { get; set; }

    public bool Situacao { get; set; }
    
    public DateTime? DataFabricacao { get; set; }    

    public DateTime? DataValidade { get; set; }

    [JsonIgnore]
    public bool IsDelete { get; set; }
}
