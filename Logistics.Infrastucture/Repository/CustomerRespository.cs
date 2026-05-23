using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using Logistics.Infrastucture.Persistence;

namespace Logistics.Infrastucture.Repository;

public class CustomerRespository : GenericRepository<Customer> , ICustomerRepository
{
    public CustomerRespository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Customer?> FindCustomerByEmailAsync(string email, CancellationToken token)
    {
        return  _Context.Customers.FirstOrDefault(c => c.Email == email);
    }
}