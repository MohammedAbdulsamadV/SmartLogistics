using Logistics.Application.Interfaces;
using Logistics.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastucture.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext _Context;

    public GenericRepository(ApplicationDbContext context)
    {
        _Context = context;
    }
    public async Task<T?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default)
    {
        return await _Context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _Context.Set<T>().ToListAsync(cancellationToken);
    }

    public void Update(T entity)
    {
        _Context.Set<T>().Update(entity);
    }
    

    public async Task AddAsync(T entity)
    {
        await _Context.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _Context.Set<T>().Remove(entity);
    }
}