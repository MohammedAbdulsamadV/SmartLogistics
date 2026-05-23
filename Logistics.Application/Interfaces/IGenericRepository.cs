namespace Logistics.Application.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    void Update(T entity);
    Task AddAsync(T entity);
    void Delete(T entity);
    
}