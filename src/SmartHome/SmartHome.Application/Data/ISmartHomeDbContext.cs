using Microsoft.EntityFrameworkCore;
using SmartHome.Domain.Device.Models;

namespace SmartHome.Application.Data
{
	public interface ISmartHomeDbContext
	{
		DbSet<SmartHome.Domain.Device.Models.Device> Devices { get; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
