using Logistics.Domain.Entities;

namespace Logistics.Application.Interfaces;

public interface IShipmentRepository : IGenericRepository<Shipment>
{
    public Task<Shipment?>  GetByOrderIdAsync(Guid orderId);
    Task<Shipment?> GetShipmentWithDetailsAsync(Guid id);
}