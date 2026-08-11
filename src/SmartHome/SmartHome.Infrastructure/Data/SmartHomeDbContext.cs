using Microsoft.EntityFrameworkCore;
using SmartHome.Domain.Device.Models;


namespace SmartHome.Infrastructure.Data
{
	public class SmartHomeDbContext :DbContext
	{
		public SmartHomeDbContext(DbContextOptions<SmartHomeDbContext> options) : base(options)
		{
		}
		public DbSet<Device> Devices { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartHomeDbContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}
	}
}
