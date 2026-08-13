using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using SmartHome.Application.Data;
using SmartHome.Domain.Device.Models;

namespace SmartHome.Application.Device.Queries.GetDevices
{
	public record GetDevicesQuery : IQuery<GetDevicesResult>;
	public record GetDevicesResult(IEnumerable<DeviceModel> Devices);
	public class GetDevicesHandler : IQueryHandler<GetDevicesQuery, GetDevicesResult>
	{
		private readonly ISmartHomeDbContext _context;
		public GetDevicesHandler(ISmartHomeDbContext context)
		{
			_context = context;
		}
		public async Task<GetDevicesResult> Handle(GetDevicesQuery query, CancellationToken cancellationToken)
		{
			var devices = await _context.Devices.AsNoTracking().ToListAsync(cancellationToken);
			return new GetDevicesResult(devices);
		}
	}
}
