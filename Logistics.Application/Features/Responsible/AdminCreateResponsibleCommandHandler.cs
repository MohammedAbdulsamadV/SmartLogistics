using Logistics.Application.Interfaces;
using MediatR;

namespace Logistics.Application.Features.Responsible;

public class AdminCreateResponsibleCommandHandler: IRequestHandler<AdminCreateResponsibleCommand, Guid>
{
    private readonly IResponsibleRepository _responsibleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdminCreateResponsibleCommandHandler(IResponsibleRepository responsibleRepository, IUnitOfWork unitOfWork)
    {
        _responsibleRepository = responsibleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(AdminCreateResponsibleCommand request, CancellationToken cancellationToken)
    {
        var responsible = new Domain.Entities.Responsible(
            request.Name, 
            request.Type, 
            request.ContactPhone, 
            request.Email, 
            request.TaxNumber, 
            request.Integration);

        await _responsibleRepository.AddAsync(responsible);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return responsible.Id;
    }
}