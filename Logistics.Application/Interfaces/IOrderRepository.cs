using Logistics.Domain.Entities;

namespace Logistics.Application.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetByTrackingNumberAsync(string trackingNumber, CancellationToken cancellationToken = default);
    Task<Order?> GetOrderWithItemsAsync(Guid id);
    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId);
}