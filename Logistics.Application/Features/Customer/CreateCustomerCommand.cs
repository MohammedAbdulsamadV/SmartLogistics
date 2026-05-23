using Logistics.Domain.Enums.Customer;
using MediatR;

namespace Logistics.Application.Features.Customer;

public record CreateCustomerCommand(string Name, string Email, CustomerType Type, string PhoneNumber, string TaxNumber, decimal CreditLimit): IRequest<Guid>;