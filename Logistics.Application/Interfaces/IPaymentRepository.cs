using Logistics.Domain.Entities;

namespace Logistics.Application.Interfaces;

public interface IPaymentRepository : IGenericRepository<Payment>
{
    Task<Payment?> GetLatestPaymentByOrderIdAsync(Guid orderId);
    Task<Payment?> GetByGatewayReferenceAsync(string transactionReference);
}