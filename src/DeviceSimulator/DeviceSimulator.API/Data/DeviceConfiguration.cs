using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

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
				.HasConversion<string>()
				.IsRequired();
			builder.Property(x=>x.Status)
				.HasConversion<string>()
				.IsRequired();

			builder.OwnsOne(x => x.State, state =>
			{
				state.Property(x => x.Values)
					.HasConversion(
						v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
						v => JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(v, (JsonSerializerOptions?)null)!)
					.HasColumnType("jsonb");
			});

			builder.OwnsMany(x => x.Capabilities, capabilities =>
			{
				capabilities.OwnsOne(x => x.Range);
				capabilities.Property(x => x.Type)
					.HasConversion<string>();
				capabilities.Property(x => x.Options)
					.HasColumnType("jsonb");
			});
		}
	}
}
