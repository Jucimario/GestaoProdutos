using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.GestaoProdutos.Entities.Base;
public class BaseEntity
{
    [Key]
    [Required]
    public int Id { get; set; }

    [JsonIgnore]
    public bool IsDelete { get; set; }
}
