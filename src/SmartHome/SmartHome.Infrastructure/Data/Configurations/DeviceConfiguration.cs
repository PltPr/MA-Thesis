using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.Domain.Device.Models;
using System.Text.Json;

namespace SmartHome.Infrastructure.Data.Configurations
{
	public class DeviceConfiguration : IEntityTypeConfiguration<Device>
	{
		public void Configure(EntityTypeBuilder<Device> builder)
		{
			var dictionaryComparer = new ValueComparer<IReadOnlyDictionary<string, JsonElement>>(
				(a, b) => a!.SequenceEqual(b!),
				a => a.Aggregate(0,
				(hash, pair) =>
					HashCode.Combine(
						hash,
						pair.Key.GetHashCode(),
						pair.Value.GetRawText(), GetHashCode())),
				a => a.ToDictionary(
					pair => pair.Key,
					pair => pair.Value));

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x=>x.Type)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x=>x.Status)
				.IsRequired()
				.HasMaxLength(20);

			builder.OwnsOne(x => x.State, state =>
			{
				state.Property(x => x.Values)
					.HasConversion(
					v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
					v => JsonSerializer.Deserialize<IReadOnlyDictionary<string, JsonElement>>(v, (JsonSerializerOptions?)null)!)
					.HasColumnName("State")
					.HasColumnType("jsonb")
					.Metadata.SetValueComparer(dictionaryComparer);
			});

			builder.OwnsMany(x => x.Capabilities, capability =>
			{
				capability.Property(x => x.Type)
					.IsRequired()
					.HasMaxLength(100);

				capability.OwnsOne(x => x.Range);

				capability.Property(x => x.Options)
					.HasConversion(
					v=> v==null ? null : JsonSerializer.Serialize(v,(JsonSerializerOptions?)null),
					v=>v==null ? null : JsonSerializer.Deserialize<List<JsonElement>>(v,(JsonSerializerOptions)null!))
					.HasColumnType("jsonb");
			});

			
				
		}
	}
}
