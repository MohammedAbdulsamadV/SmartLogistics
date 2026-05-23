using Logistics.Application.DTOs;
using Logistics.Application.Interfaces;
using Logistics.Domain.Enums.Payment;
using MediatR;

namespace Logistics.Application.Features.Payment.Commands.InitializePayment;

public class InitializePaymentCommandHandler : IRequestHandler<InitializePaymentCommand,PaymentInitResultDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentGatewayService _gatewayService; 
    private readonly IUnitOfWork _unitOfWork;

    public InitializePaymentCommandHandler(
        IOrderRepository orderRepository,
        IPaymentRepository paymentRepository,
        IPaymentGatewayService gatewayService,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _paymentRepository = paymentRepository;
        _gatewayService = gatewayService;
        _unitOfWork = unitOfWork;
    }
    public async Task<PaymentInitResultDto> Handle(InitializePaymentCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderWithItemsAsync(request.OrderId);
        if (order == null) throw new Exception("Order not found");

        var method = (PaymentMethod)request.PaymentMethodNumber;
        var payment = new Domain.Entities.Payment(order.Id, order.GetTotalPrice(), method);

        // نداء الـ الخدمة الخارجية المعزولة في الـ Infrastructure
        var gatewayResult = await _gatewayService.CreateCheckoutSessionAsync(
            order.Id, 
            order.GetTotalPrice(), 
            request.PaymentProviderName);

        if (!gatewayResult.IsSuccess)
            throw new Exception($"Failed to Create {gatewayResult.ErrorMessage}");

        payment.SetGatewayInfo(request.PaymentProviderName, gatewayResult.TransactionReference, gatewayResult.CheckoutUrl);

        await _paymentRepository.AddAsync(payment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var result = new PaymentInitResultDto
        {
            CheckoutUrl = gatewayResult.CheckoutUrl,
            TransactionReference = gatewayResult.TransactionReference,
            Amount = order.GetTotalPrice()
        };

        return result;
    }
}