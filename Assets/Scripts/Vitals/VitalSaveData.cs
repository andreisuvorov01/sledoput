using System;

namespace Vitals
{
	[Serializable]
	public class VitalSaveData
	{
		public float value;

		public float maxValue;

		public VitalSaveData(float value, float maxValue)
		{
			this.value = value;
			this.maxValue = maxValue;
		}
	}
}
