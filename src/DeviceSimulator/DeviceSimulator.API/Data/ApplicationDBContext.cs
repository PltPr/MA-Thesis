using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace DeviceSimulator.API.Data
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options) 
			: base(options)
		{
		}
		public DbSet<DeviceEntity> Devices { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
		}
	}
}
