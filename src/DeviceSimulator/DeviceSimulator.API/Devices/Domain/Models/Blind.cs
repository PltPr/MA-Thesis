using DeviceSimulator.API.Devices.Domain.Events;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace DeviceSimulator.API.Devices.Domain.Models
{
	public class Blind :Device
	{
		protected Blind(Guid id, string name, DeviceType type, DeviceStatus status, DeviceState state, List<Capability> capabilities)
			:base(id,name,type,status,state,capabilities)
		{
			
		}

		public static Blind Create(Guid id, string name, DeviceStatus status, DeviceState state, List<Capability> capabilities)
		{
			return new Blind(id, name, DeviceType.Blind, status, state, capabilities);
		}

		public override void SetCapability(CapabilityType type, JsonElement value)
		{
			switch (type)
			{
				case CapabilityType.Position:
					SetPosition(value.GetInt32());
					break;

				default:
					throw new NotSupportedException(
						$"Capability \"{type}\" is not supported by Blind.");

			}
		}

		private void SetPosition(int value)
		{
			State.Set(CapabilityType.Position, value);
		}
	}
}
