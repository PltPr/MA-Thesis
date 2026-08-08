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

		public Thermometer Create(Guid id, string name, DeviceStatus status, DeviceState state, List<Capability> capabilities)
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
							$"Capability {type} is not supported by Light.");
			}
		}

		public void SimulateTemperature(decimal temperature)
		{
			var accTemp = State.Get<decimal>(CapabilityType.Temperature);
			if (accTemp == temperature)
				return;
			State.Set(CapabilityType.Temperature, temperature);
			//send event
		}
	}
}
