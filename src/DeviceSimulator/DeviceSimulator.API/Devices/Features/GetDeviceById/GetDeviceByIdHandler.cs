
using DeviceSimulator.API.Data;
using DeviceSimulator.API.Devices.Domain.Factories;
using Microsoft.EntityFrameworkCore;

namespace DeviceSimulator.API.Devices.Features.GetDeviceById
{
	public record GetDeviceByIdQuery(Guid Id) :IQuery<GetDeviceByIdResult>;
	public record GetDeviceByIdResult(Device Device);
	public class GetDeviceByIdHandler : IQueryHandler<GetDeviceByIdQuery, GetDeviceByIdResult>
	{
		private readonly ApplicationDBContext _context;
		private readonly IDeviceFactory _factory;
		public GetDeviceByIdHandler(ApplicationDBContext context, IDeviceFactory factory)
		{
			_context = context;
			_factory = factory;
		}
		public async Task<GetDeviceByIdResult> Handle(GetDeviceByIdQuery query, CancellationToken cancellationToken)
		{
			var deviceEntity = await _context.Devices.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==query.Id);
			if (deviceEntity == null)
				throw new ArgumentException("Not Found");
			var result = _factory.Create(deviceEntity);

			return new GetDeviceByIdResult(result);
		}
	}
}
