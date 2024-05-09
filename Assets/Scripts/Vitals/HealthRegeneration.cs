using UnityEngine;

namespace Vitals
{
	public class HealthRegeneration : RegenerationBase
	{
		[SerializeField]
		private Health healthComponent;

		private void Awake()
		{
			VitalsComponent = healthComponent;
		}
	}
}
