using Application.GestaoProdutos.Context;
using Application.GestaoProdutos.Services.v1.Interfaces;
using Domain.GestaoProdutos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.GestaoProdutos.Services.v1
{
    public class ProdutoService : IProdutoService
    {

        private AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Produto> Create(Produto produto)
        {
            if (produto is null)
                return null;

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return produto;
        }

        public async Task<bool> Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return false;
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return true;
        }

        public async Task<Produto> Disable(int id)
        {

            var produto = _context.Produtos.SingleOrDefault(p => p.Id.Equals(id));
            if (produto == null)
            {
                return null;
            }

            produto.Situacao = false;
            try
            {
                _context.Entry(produto).CurrentValues.SetValues(produto);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return produto;
        }

        public async Task<ICollection<Produto>> FindAll(int skip = 0, int take = 50)
        {
            return _context.Produtos.AsNoTracking().Where(p => p.Situacao).Skip(skip).Take(take).ToList();
        }

        public async Task<Produto> FindByID(int id)
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (produto == null)
                return null;          

            return produto;
        }

        public async Task<Produto> Update(Produto item)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == item.Id);

            if (produto == null)
                return null;            

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return produto;
        }
    }
}
