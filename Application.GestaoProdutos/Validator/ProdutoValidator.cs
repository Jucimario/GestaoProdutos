using Domain.GestaoProdutos.Entities;
using FluentValidation;

namespace Application.GestaoProdutos.Validator;

public class ProdutoValidator : AbstractValidator<Produto>
{
    public ProdutoValidator()
    {
        RuleFor(p => p.Descricao)
            .NotEmpty()
            .WithMessage("Campo Obrigarório");

        RuleFor(p => p.DataFabricacao)
          .LessThan(a => a.DataValidade)
          .WithMessage("Data de Fabricação deve ser menor que a data de Validade");
    }
}
