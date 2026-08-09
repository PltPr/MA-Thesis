using DeviceSimulator.API.Devices.Domain.Events;
using System.Text.Json;

namespace DeviceSimulator.API.Devices.Domain.Models
{
	public class DimmableLight :Light
	{
		protected DimmableLight(
			Guid id,
			string name,
			DeviceType type,
			DeviceStatus status,
			DeviceState state,
			List<Capability> capabilities
			)
			: base(id, name, type, status, state, capabilities)
		{
			
		}

		public static new DimmableLight Create(Guid id,string name, DeviceStatus status, DeviceState state, List<Capability> capabilities )
		{
			return new DimmableLight(
				id,
				name,
				DeviceType.DimmableLight,
				status,
				state,
				capabilities
			);
		}

		public override void SetCapability(CapabilityType type, JsonElement value)
		{
			switch(type)
			{
				case CapabilityType.Brightness:
					SetBrightness(value.GetInt32());
					break;

				default:
					base.SetCapability(type, value);
					break;
			}
		}

		public void SetBrightness(int brightness)
		{
			State.Set(CapabilityType.Brightness, brightness);
		}
	}
}
