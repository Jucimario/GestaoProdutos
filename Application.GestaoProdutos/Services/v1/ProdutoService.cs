using Application.GestaoProdutos.Context;
using Application.GestaoProdutos.Services.v1.Generic;
using Application.GestaoProdutos.Services.v1.Interfaces;
using Domain.GestaoProdutos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.GestaoProdutos.Services.v1;

public class ProdutoService : GenericService<Produto>, IProdutoService
{
    public ProdutoService(AppDbContext context) : base(context) { }

    public async Task<ICollection<Produto>> FindAll(int skip = 0, int take = 50)
    {
        return _context.Produtos.AsNoTracking().Where(p => p.Situacao).Skip(skip).Take(take).ToList();
    }

    public async Task<Produto?> Disable(int id)
    {
        try
        {
            var produto = _context.Produtos.SingleOrDefault(p => p.Id.Equals(id));
            if (produto == null)
                return null;           

            produto.Situacao = false;

            _context.Entry(produto).CurrentValues.SetValues(produto);
            _context.SaveChanges();

            return produto;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
