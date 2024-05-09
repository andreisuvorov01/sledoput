using UnityEngine;
using UnityEngine.Events;

namespace Vitals
{
	public class VitalsEventWrapper : MonoBehaviour
	{
		[SerializeField]
		private VitalsBase vitalsComponent;

		[Header("[Optional]")]
		[SerializeField]
		private RegenerationBase regenerationComponent;

		[Space(10f)]
		public UnityEvent onValueIncrease;

		public UnityEvent onValueDecrease;

		public UnityEvent onMaxValueIncrease;

		public UnityEvent onMaxValueDecrease;

		public UnityEvent onValueFull;

		public UnityEvent onValueEmpty;

		public UnityEvent onRegenerationStart;

		public UnityEvent onRegenerationStop;

		public UnityEvent onDrainStart;

		public UnityEvent onDrainStop;

		private void OnEnable()
		{
			if (vitalsComponent == null)
			{
				Debug.LogError("Vitals component is null");
				return;
			}
			vitalsComponent.OnValueIncrease += OnIncrease;
			vitalsComponent.OnValueDecrease += OnDecrease;
			vitalsComponent.OnMaxValueIncrease += OnMaxIncrease;
			vitalsComponent.OnMaxValueDecrease += OnMaxDecrease;
			vitalsComponent.OnValueEmpty += OnDeath;
			vitalsComponent.OnValueFull += OnFullVitals;
			if (!(regenerationComponent == null))
			{
				regenerationComponent.OnRegenerationStatusChanged += OnRegenerationStatusChanged;
				regenerationComponent.OnDrainStatusChanged += OnDrainStatusChanged;
			}
		}

		private void OnDisable()
		{
			if (vitalsComponent == null)
			{
				Debug.LogError("Vitals component is null");
				return;
			}
			vitalsComponent.OnValueIncrease -= OnIncrease;
			vitalsComponent.OnValueDecrease -= OnDecrease;
			vitalsComponent.OnMaxValueIncrease -= OnMaxIncrease;
			vitalsComponent.OnMaxValueDecrease -= OnMaxDecrease;
			vitalsComponent.OnValueEmpty -= OnDeath;
			vitalsComponent.OnValueFull -= OnFullVitals;
			if (!(regenerationComponent == null))
			{
				regenerationComponent.OnRegenerationStatusChanged -= OnRegenerationStatusChanged;
				regenerationComponent.OnDrainStatusChanged -= OnDrainStatusChanged;
			}
		}

		private void OnIncrease()
		{
			onValueIncrease?.Invoke();
		}

		private void OnDecrease()
		{
			onValueDecrease?.Invoke();
		}

		private void OnMaxIncrease()
		{
			onMaxValueIncrease?.Invoke();
		}

		private void OnMaxDecrease()
		{
			onMaxValueDecrease?.Invoke();
		}

		private void OnFullVitals()
		{
			onValueFull?.Invoke();
		}

		private void OnDeath()
		{
			onValueEmpty?.Invoke();
		}

		private void OnRegenerationStatusChanged(bool status)
		{
			if (status)
			{
				onRegenerationStart?.Invoke();
			}
			else
			{
				onRegenerationStop?.Invoke();
			}
		}

		private void OnDrainStatusChanged(bool status)
		{
			if (status)
			{
				onDrainStart?.Invoke();
			}
			else
			{
				onDrainStop?.Invoke();
			}
		}
	}
}
