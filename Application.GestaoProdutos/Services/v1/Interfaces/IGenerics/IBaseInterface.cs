using Domain.GestaoProdutos.Entities.Base;

namespace Application.GestaoProdutos.Services.v1.Interfaces.IGenerics;

public interface IBaseInterface<T> where T : BaseEntity
{
    public Task<T> FindByID(int id);

    public Task<T> Create(T item);

    public Task<T> Update(T item);

    public Task<bool> Delete(int id);
}
