using System;

namespace Vitals
{
	public interface IVitalsComponent
	{
		float Value { get; }

		float MaxValue { get; }

		event Action OnValueIncrease;

		event Action OnValueDecrease;

		event Action OnMaxValueIncrease;

		event Action OnMaxValueDecrease;

		event Action OnValueFull;

		event Action OnValueEmpty;

		void Increase(float amount, bool triggerEvents = true, bool isDrain = false);

		void Decrease(float amount, bool triggerEvents = true, bool isDrain = false);

		void IncreaseMax(float amount);

		void DecreaseMax(float amount);

		void Set(float value);

		void SetMax(float value);
	}
}
