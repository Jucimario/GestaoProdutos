using Application.GestaoProdutos.Context;
using Application.GestaoProdutos.Services.v1.Interfaces.IGenerics;
using Domain.GestaoProdutos.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Application.GestaoProdutos.Services.v1.Generic;

public class GenericService<T> : IBaseInterface<T> where T : BaseEntity
{
    protected AppDbContext _context;
    private DbSet<T> _dataset;

    public GenericService(AppDbContext context)
    {
        _context = context;
        _dataset = _context.Set<T>();
    }

    public async Task<T> Create(T item)
    {
        try
        {
            _dataset.Add(item);
            _context.SaveChanges();
            return item;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var result = _dataset.SingleOrDefault(p => p.Id.Equals(id));
            if (result == null)
                return false;

            _dataset.Remove(result);
            _context.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<T?> FindByID(int id)
    {
        return _dataset.AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));       
    }

    public async Task<T?> Update(T item)
    {
        try
        {
            var result = _dataset.SingleOrDefault(p => p.Id.Equals(item.Id));
            if (result == null)
                return null;

            _context.Entry(result).CurrentValues.SetValues(item);
            _context.SaveChanges();
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
