using System.Text.Json;

namespace SmartHome.Domain.Device.Models
{
	public class Device
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = default!;
		public string Type { get; private set; } = default!;
		public string Status { get; private set; } = default!;
		public DeviceState State { get; private set; } = default!;
		private readonly List<Capability> _capabilities = [];
		public IReadOnlyCollection<Capability> Capabilities => _capabilities.AsReadOnly();

		private Device() { }

		private Device(Guid id,
		string name,
		string type,
		string status,
		DeviceState state,
		IEnumerable<Capability> capabilities)
		{
			Id = id;
			Name = name;
			Type = type;
			Status = status;
			State = state;
			_capabilities.AddRange(capabilities);
		}

		public static Device Create(
		Guid id,
		string name,
		string type,
		string status,
		DeviceState state,
		IEnumerable<Capability> capabilities)
		{
			ArgumentOutOfRangeException.ThrowIfEqual(id, Guid.Empty);

			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException(
					"Device name is required.",
					nameof(name));

			if (string.IsNullOrWhiteSpace(type))
				throw new ArgumentException(
					"Device type is required.",
					nameof(type));

			if (string.IsNullOrWhiteSpace(status))
				throw new ArgumentException(
					"Device status is required.",
					nameof(status));

			ArgumentNullException.ThrowIfNull(state);
			ArgumentNullException.ThrowIfNull(capabilities);

			return new Device(
				id,
				name,
				type,
				status,
				state,
				capabilities);
		}
		
		public void UpdateCapability(string capability,JsonElement value)
		{
			if (!HasCapability(capability))
				throw new InvalidOperationException(
					$"Device does not have capability '{capability}'.");

			State.Set(capability,value);
		}

		public void UpdateStatus(string status)
		{
			if(status!="Online" || status!="Offline")
				throw new ArgumentException("Invalid status type");

			if (string.IsNullOrWhiteSpace(status))
				throw new ArgumentException(
					"Device status cannot be empty.");

			Status = status;
		}

		public bool HasCapability (string capability)
		{
			return _capabilities.Any(x=>x.Type.Equals(capability,StringComparison.OrdinalIgnoreCase));
		}
	}
}
