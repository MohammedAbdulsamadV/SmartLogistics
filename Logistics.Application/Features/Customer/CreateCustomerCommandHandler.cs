using Logistics.Application.Interfaces;
using MediatR;

namespace Logistics.Application.Features.Customer;

public class CreateCustomerCommandHandler: IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly IGenericRepository<Domain.Entities.Customer> _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(IGenericRepository<Domain.Entities.Customer> customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Domain.Entities.Customer(request.Name, request.Email, request.Type, request.PhoneNumber, request.TaxNumber, request.CreditLimit);
        await _customerRepository.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return customer.Id;
    }
}