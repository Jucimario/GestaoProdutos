using Domain.GestaoProdutos.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GestaoProdutos.Validator
{
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
}
