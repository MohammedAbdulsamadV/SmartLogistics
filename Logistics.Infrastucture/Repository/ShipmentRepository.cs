using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using Logistics.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastucture.Repository;

public class ShipmentRepository : GenericRepository<Shipment> , IShipmentRepository
{
    public ShipmentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Shipment?> GetByOrderIdAsync(Guid orderId)
    {
        return await _Context.Shipments
            .AsNoTracking().Include(s => s.ShipmentLegs)
            .FirstOrDefaultAsync(s => s.OrderId == orderId);
    }

    public async Task<Shipment?> GetShipmentWithDetailsAsync(Guid id)
    {
        return await _Context.Shipments
            .AsNoTracking().Include(s => s.ShipmentLegs)
            .ThenInclude(r => r.Responsible)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}