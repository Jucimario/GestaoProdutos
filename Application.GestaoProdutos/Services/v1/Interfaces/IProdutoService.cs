using Domain.GestaoProdutos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GestaoProdutos.Services.v1.Interfaces
{
    public interface IProdutoService
    {
        public Task<ICollection<Produto>> FindAll(int skip = 0, int take = 50);

        public Task<Produto> FindByID(int id);

        public Task<Produto> Create(Produto item);

        public Task<Produto> Update(Produto item);

        public Task<bool> Delete(int id);       

        public Task<Produto> Disable(int id);
    }
}
