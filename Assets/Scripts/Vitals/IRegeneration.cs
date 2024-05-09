using System;

namespace Vitals
{
	public interface IRegeneration
	{
		float RegenerationRate { get; }

		bool IsRegenerating { get; }

		float DrainRate { get; }

		bool IsDraining { get; }

		event Action<bool> OnRegenerationStatusChanged;

		event Action<bool> OnDrainStatusChanged;

		void StartRegeneration();

		void StopRegeneration();

		void StartDrain();

		void StopDrain();

		void SetRegenerationRate(float rate);

		void SetDrainRate(float rate);
	}
}
