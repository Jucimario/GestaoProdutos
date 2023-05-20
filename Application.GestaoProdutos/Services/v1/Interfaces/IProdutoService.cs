using Application.GestaoProdutos.Services.v1.Interfaces.IGenerics;
using Domain.GestaoProdutos.Entities;

namespace Application.GestaoProdutos.Services.v1.Interfaces;

public interface IProdutoService : IBaseInterface<Produto>
{
    public Task<ICollection<Produto>> FindAll(int skip = 0, int take = 50);     

    public Task<Produto> Disable(int id);
}
