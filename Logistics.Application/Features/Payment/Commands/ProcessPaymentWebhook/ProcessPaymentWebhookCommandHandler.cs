using Logistics.Application.Interfaces;
using MediatR;

namespace Logistics.Application.Features.Payment.Commands.ProcessPaymentWebhook;

public class ProcessPaymentWebhookCommandHandler : IRequestHandler<ProcessPaymentWebhookCommand,bool>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProcessPaymentWebhookCommandHandler(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
    {
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(ProcessPaymentWebhookCommand request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByGatewayReferenceAsync(request.TransactionReference);
        if (payment == null) throw new Exception("Payment process not registered.");

        if (request.GatewayStatus.ToUpper() == "SUCCESS")
        {
            payment.Complete(); 
        }

        _paymentRepository.Update(payment);
        await _unitOfWork.SaveChangesAsync(cancellationToken); 

        return true;
    }
}