using System.Threading.Tasks;
using UnityEngine;

namespace Vitals
{
	public static class VitalsUtility
	{
		public static float GetPercentage(VitalsBase vitals)
		{
			return vitals.Value / vitals.MaxValue;
		}

		public static float GetPercentage(VitalsHook vitals)
		{
			return vitals.Value / vitals.MaxValue;
		}

		public static float GetPercentage(float value, float maxValue)
		{
			return value / maxValue;
		}

		public static void IncreaseByPercentage(VitalsBase vitals, float percentage)
		{
			if (!IsValidPercentage(percentage))
			{
				Debug.Log("Invalid percentage value. Percentage must be between 0 and 1.");
			}
			else
			{
				vitals.Increase(vitals.Value * percentage);
			}
		}

		public static void DecreaseByPercentage(VitalsBase vitals, float percentage)
		{
			if (!IsValidPercentage(percentage))
			{
				Debug.Log("Invalid percentage value. Percentage must be between 0 and 1.");
			}
			else
			{
				vitals.Decrease(vitals.Value * percentage);
			}
		}

		public static void IncreaseMaxByPercentage(VitalsBase vitals, float percentage)
		{
			if (!IsValidPercentage(percentage))
			{
				Debug.Log("Invalid percentage value. Percentage must be between 0 and 1.");
			}
			else
			{
				vitals.IncreaseMax(vitals.MaxValue * percentage);
			}
		}

		public static void DecreaseMaxByPercentage(VitalsBase vitals, float percentage)
		{
			if (!IsValidPercentage(percentage))
			{
				Debug.Log("Invalid percentage value. Percentage must be between 0 and 1.");
			}
			else
			{
				vitals.DecreaseMax(vitals.MaxValue * percentage);
			}
		}

		public static async void DrainAtRateForTime(VitalsBase vitals, float drainRate, float drainTime)
		{
			if (!IsValidAmount(drainRate))
			{
				Debug.Log("Invalid drain rate value. Drain rate must be bigger than 0.");
				return;
			}
			if (!IsValidAmount(drainTime))
			{
				Debug.Log("Invalid drain time value. Drain time must be bigger than 0.");
				return;
			}
			if (!vitals.Regeneration)
			{
				Debug.Log("Vitals component does not have the regeneration extension.");
				return;
			}
			float initialDrainRate = vitals.Regeneration.DrainRate;
			vitals.Regeneration.SetDrainRate(drainRate);
			vitals.Regeneration.StartDrain();
			await Task.Delay((int)(drainTime * 1000f));
			vitals.Regeneration.StopDrain();
			vitals.Regeneration.SetDrainRate(initialDrainRate);
		}

		public static async void RegenerateAtRateForTime(VitalsBase vitals, float regenerationRate, float regenerationTime)
		{
			if (!IsValidAmount(regenerationRate))
			{
				Debug.Log("Invalid regeneration rate value. Regeneration rate must be bigger than 0.");
				return;
			}
			if (!IsValidAmount(regenerationTime))
			{
				Debug.Log("Invalid regeneration time value. Regeneration time must be bigger than 0.");
				return;
			}
			if (!vitals.Regeneration)
			{
				Debug.Log("Vitals component does not have the regeneration extension.");
				return;
			}
			float initialRegenerationRate = vitals.Regeneration.RegenerationRate;
			vitals.Regeneration.SetRegenerationRate(regenerationRate);
			vitals.Regeneration.StartRegeneration();
			await Task.Delay((int)(regenerationTime * 1000f));
			vitals.Regeneration.StopRegeneration();
			vitals.Regeneration.SetRegenerationRate(initialRegenerationRate);
		}

		public static bool IsValidPercentage(float percentage)
		{
			if (percentage >= 0f)
			{
				return percentage <= 1f;
			}
			return false;
		}

		public static bool IsValidAmount(float amount)
		{
			return amount >= 0f;
		}

		public static bool IsFull(VitalsBase vitals)
		{
			return vitals.Value >= vitals.MaxValue;
		}

		public static bool IsEmpty(VitalsBase vitals)
		{
			return vitals.Value <= 0f;
		}

		public static void Bind(VitalsBase vitals, VitalsUIBind vitalsUIBind)
		{
			VitalsHook vitalsHook = ScriptableObject.CreateInstance<VitalsHook>();
			vitalsUIBind.Initialize(vitalsHook);
			vitals.SetHook(vitalsHook);
			vitalsHook.Initialize(vitals);
		}
	}
}
