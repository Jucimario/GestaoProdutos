using System.ComponentModel.DataAnnotations;

namespace Domain.GestaoProdutos.Entities.Base;
public class BaseEntity
{
    [Key]
    [Required]
    public int Id { get; set; }
  
}
