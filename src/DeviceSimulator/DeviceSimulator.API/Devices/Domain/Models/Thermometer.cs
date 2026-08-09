using DeviceSimulator.API.Devices.Domain.Events;
using System.Text.Json;

namespace DeviceSimulator.API.Devices.Domain.Models
{
	public class Thermometer : Device
	{
		private Thermometer(
		Guid id,
		string name,
		DeviceType type,
		DeviceStatus status,
		DeviceState state,
		List<Capability> capabilities)
		: base(
			id, name, type, status, state, capabilities)
		{
		}

		public static Thermometer Create(Guid id, string name, DeviceStatus status, DeviceState state, List<Capability> capabilities)
		{
			return new Thermometer(id,name,DeviceType.Thermometer,status,state,capabilities);
		}


		public override void SetCapability(CapabilityType type, JsonElement value)
		{
			switch (type)
			{
				case CapabilityType.Temperature:
					throw new InvalidOperationException("Temperature is read-only on a thermometer.");

				default:
					throw new NotSupportedException(
						$"Capability {type} is not supported by thermometer.");
			}
		}

		public override void SimulateCapability(CapabilityType type, JsonElement value)
		{
			if(!State.HasChanged(type,value)) return;
			switch (type)
			{
				case CapabilityType.Temperature:
					SimulateTemperature(value.GetDecimal());
					break;
				default:
					throw new NotSupportedException(
						$"Capability {type} is not supported by thermometer.");
			}
			AddDomainEvent(new DeviceStateChangedDomainEvent(
				Id, type, value));

		}


		public void SimulateTemperature(decimal temperature)
		{
			State.Set(CapabilityType.Temperature, temperature);
		}
	}
}
