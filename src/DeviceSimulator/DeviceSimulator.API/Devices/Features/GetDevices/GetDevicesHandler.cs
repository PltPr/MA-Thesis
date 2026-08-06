
using DeviceSimulator.API.Data;
using DeviceSimulator.API.Devices.Domain.Factories;
using Microsoft.EntityFrameworkCore;

namespace DeviceSimulator.API.Devices.Features.GetDevices
{
	public record GetDevicesQuery() :IQuery<GetDevicesResult>;
	public record GetDevicesResult(IEnumerable<Device> Devices);
	public class GetDevicesHandler 
		: IQueryHandler<GetDevicesQuery, GetDevicesResult>
	{
		private readonly ApplicationDBContext _context;
		private readonly IDeviceFactory _factory;
		public GetDevicesHandler(ApplicationDBContext context,IDeviceFactory factory)
		{
			_context = context;
			_factory = factory;
		}
		public async Task<GetDevicesResult> Handle(GetDevicesQuery query, CancellationToken cancellationToken)
		{
			var deviceEntities = await _context.Devices.ToListAsync(cancellationToken);
			var devices = deviceEntities.Select(x=>_factory.Create(x)).ToList();
			return new GetDevicesResult(devices);
		}
	}
}
