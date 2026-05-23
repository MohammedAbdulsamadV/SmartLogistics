using Logistics.Application.Interfaces;
using Logistics.Domain.ValueObjects.Integrations;
using MediatR;

namespace Logistics.Application.Features.Responsible;

public class AdminUpdateShippingIntegrationCommandHandler : IRequestHandler<AdminUpdateShippingIntegrationCommand, bool>
{
    private readonly IResponsibleRepository _responsibleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdminUpdateShippingIntegrationCommandHandler(IResponsibleRepository responsibleRepository, IUnitOfWork unitOfWork)
    {
        _responsibleRepository = responsibleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AdminUpdateShippingIntegrationCommand request, CancellationToken cancellationToken)
    {
        var responsible = await _responsibleRepository.GetByIdAsync(request.ResponsibleId);
        if (responsible == null) throw new Exception("Responsible not found.");

        var integration = new ShippingIntegration(
            request.Provider, 
            request.ApiKey, 
            request.WebhookUrl, 
            request.ApiBaseUrl);

        responsible.UpdateDetails(
            responsible.Name, 
            responsible.ContactPhone, 
            responsible.Email, 
            responsible.TaxNumber, 
            integration);

        _responsibleRepository.Update(responsible);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}