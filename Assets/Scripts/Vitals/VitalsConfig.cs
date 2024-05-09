using UnityEngine;

namespace Vitals
{
	[CreateAssetMenu(fileName = "Vitals Config", menuName = "Vitals/New Config", order = 0)]
	public class VitalsConfig : ScriptableObject
	{
		[Header("Values")]
		public float maxValue;

		public float value;

		private void OnValidate()
		{
			if (value > maxValue)
			{
				value = maxValue;
			}
		}
	}
}
