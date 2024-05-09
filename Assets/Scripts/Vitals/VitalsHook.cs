using System;
using UnityEngine;

namespace Vitals
{
	[CreateAssetMenu(fileName = "Vitals Hook", menuName = "Vitals/New Hook", order = 0)]
	public class VitalsHook : ScriptableObject
	{
		private bool _initialized;

		public bool Initialized => _initialized;

		public float Value { get; private set; }

		public float MaxValue { get; private set; }

		public event Action<float, float, bool> OnValueChanged;

		public void Initialize(VitalsBase vitalsBase)
		{
			vitalsBase.OnValueChanged += UpdateValues;
			_initialized = true;
			UpdateValues(vitalsBase.Value, vitalsBase.MaxValue, isDrain: false);
		}

		public void Unsubscribe(VitalsBase vitalsBase)
		{
			vitalsBase.OnValueChanged -= UpdateValues;
			_initialized = false;
		}

		private void UpdateValues(float value, float maxValue, bool isDrain)
		{
			Value = value;
			MaxValue = maxValue;
			this.OnValueChanged?.Invoke(value, maxValue, isDrain);
		}
	}
}
