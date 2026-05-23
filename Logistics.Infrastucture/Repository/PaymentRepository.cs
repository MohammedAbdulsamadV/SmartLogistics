using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using Logistics.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastucture.Repository;

public class PaymentRepository : GenericRepository<Payment> , IPaymentRepository
{
    public PaymentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Payment?> GetLatestPaymentByOrderIdAsync(Guid orderId)
    {
        return  await _Context.Payments
            .AsNoTracking().Where(p => p.OrderId == orderId).OrderByDescending(p => p.CreatedAt)
            .FirstOrDefaultAsync();;
    }

    public async Task<Payment?> GetByGatewayReferenceAsync(string transactionReference)
    {
        return await _Context.Set<Payment>()
            .FirstOrDefaultAsync(p => p.GatewayDetails!.Reference == transactionReference);
    }
}