using Logistics.Application.Interfaces;
using MediatR;
using MediatR.Pipeline;

namespace Logistics.Application.Features.Payment.Commands;

public class CompletePaymentHandler : IRequestHandler<CompletePaymentCommand,bool>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CompletePaymentHandler(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
    {
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(CompletePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = new Domain.Entities.Payment(request.OrderId, request.Amount,request.Method);
        
        payment.Complete(); 

        await _paymentRepository.AddAsync(payment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}