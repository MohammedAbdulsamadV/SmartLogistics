using Logistics.Domain.Entities;

namespace Logistics.Application.Interfaces;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<Customer?> FindCustomerByEmailAsync(string email, CancellationToken token);
    
}