using System;
using UnityEngine;

namespace Vitals
{
	public abstract class RegenerationBase : MonoBehaviour, IRegeneration
	{
		protected VitalsBase VitalsComponent;

		[field: SerializeField]
		[field: Header("Configuration")]
		public float RegenerationRate { get; private set; }

		public bool IsRegenerating { get; private set; }

		[field: SerializeField]
		public float DrainRate { get; private set; }

		public bool IsDraining { get; private set; }

		public event Action<bool> OnRegenerationStatusChanged;

		public event Action<bool> OnDrainStatusChanged;

		private void OnEnable()
		{
			if (!VitalsComponent)
			{
				Debug.LogError("Regeneration Component is missing Vitals Component on " + base.gameObject.name + " game object.");
			}
			else
			{
				VitalsComponent.SetRegeneration(this);
			}
		}

		private void Update()
		{
			if (IsRegenerating)
			{
				Regenerate();
			}
			if (IsDraining)
			{
				Drain();
			}
		}

		public void StartRegeneration()
		{
			StopDrain();
			IsRegenerating = true;
			this.OnRegenerationStatusChanged?.Invoke(IsRegenerating);
		}

		public void StopRegeneration()
		{
			IsRegenerating = false;
			this.OnRegenerationStatusChanged?.Invoke(IsRegenerating);
		}

		public void StartDrain()
		{
			StopRegeneration();
			IsDraining = true;
			this.OnDrainStatusChanged?.Invoke(IsDraining);
		}

		public void StopDrain()
		{
			IsDraining = false;
			this.OnDrainStatusChanged?.Invoke(IsDraining);
		}

		public void SetRegenerationRate(float rate)
		{
			RegenerationRate = rate;
		}

		public void SetDrainRate(float rate)
		{
			DrainRate = rate;
		}

		private void Regenerate()
		{
			float amount = RegenerationRate * Time.deltaTime;
			VitalsComponent.Increase(amount, triggerEvents: false, isDrain: true);
			if (VitalsUtility.IsFull(VitalsComponent))
			{
				StopRegeneration();
			}
		}

		private void Drain()
		{
			float amount = DrainRate * Time.deltaTime;
			VitalsComponent.Decrease(amount, triggerEvents: false, isDrain: true);
			if (VitalsUtility.IsEmpty(VitalsComponent))
			{
				StopDrain();
			}
		}
	}
}
