using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using Logistics.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastucture.Repository;

public class OrderRepository : GenericRepository<Order> , IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Order?> GetByTrackingNumberAsync(string trackingNumber, CancellationToken cancellationToken = default)
    {
        return await  _Context.Orders.AsNoTracking().Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.TrackingNumber == trackingNumber, cancellationToken);
    }

    public async Task<Order?> GetOrderWithItemsAsync(Guid id)
    {
        return await _Context.Orders
            .AsNoTracking().Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId)
    {
        return await _Context.Orders
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }
}