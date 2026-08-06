namespace DeviceSimulator.API.Devices.Domain.Models
{
	public class Light : Device
	{

		protected Light(
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

		public static Light Create(Guid id,string name,DeviceStatus status, DeviceState state, List<Capability>capabilities)
		{
			return new Light(id,
				name,
				DeviceType.Light,
				status,
				state,
				capabilities
			);
		}
		public void TurnOn()
		{
			State.Set(CapabilityType.Power,true);
		}
		public void TurnOff()
		{
			State.Set(CapabilityType.Power, false);
		}
	}
}
