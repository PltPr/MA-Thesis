using Microsoft.EntityFrameworkCore;
using SmartHome.Domain.Device.Models;

namespace SmartHome.Application.Data
{
	public interface ISmartHomeDbContext
	{
		DbSet<Device> Devices { get; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
