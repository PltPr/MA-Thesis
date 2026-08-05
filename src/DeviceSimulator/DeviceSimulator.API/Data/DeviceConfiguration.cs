using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeviceSimulator.API.Data
{
	public class DeviceConfiguration : IEntityTypeConfiguration<DeviceEntity>
	{
		public void Configure(EntityTypeBuilder<DeviceEntity> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(100);
			builder.Property(x => x.Type)
				.IsRequired();
			builder.Property(x=>x.Status)
				.IsRequired();

			builder.OwnsOne(x => x.State, state =>
			{
				state.Property(x => x.Values)
					.HasColumnType("jsonb");
			});

			builder.OwnsMany(x => x.Capabilities, capabilities =>
			{
				capabilities.OwnsOne(x => x.Range);
			});
		}
	}
}
