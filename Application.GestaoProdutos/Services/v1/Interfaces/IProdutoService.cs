using Application.GestaoProdutos.Services.v1.Interfaces.IGenerics;
using Domain.GestaoProdutos.Dtos.ProdutoDtos;
using Domain.GestaoProdutos.Entities;

namespace Application.GestaoProdutos.Services.v1.Interfaces;

public interface IProdutoService : IBaseInterface<Produto>
{
    public Task<FilterProdutoDto> FindAll(string nome, int skip, int take);     

}
