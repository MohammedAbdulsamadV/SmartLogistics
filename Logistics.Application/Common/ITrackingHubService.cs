namespace Logistics.Application.Common;

public interface ITrackingHubService
{
    Task SendLocationUpdateAsync(Guid shipmentId, decimal latitude, decimal longitude);
}