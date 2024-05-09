using System;
using UnityEngine;

namespace Vitals
{
	[DefaultExecutionOrder(-10)]
	public abstract class VitalsBase : MonoBehaviour, IVitalsComponent
	{
		[Header("Config")]
		[SerializeField]
		private VitalsConfig vitalsConfig;

		[Header("Settings")]
		[SerializeField]
		private bool saveOnQuit;

		[SerializeField]
		private bool loadOnStart;

		[Header("[Optional]")]
		[SerializeField]
		private VitalsHook vitalsHook;

		private RegenerationBase _regeneration;

		public VitalsConfig VitalsConfig => vitalsConfig;

		public float Value { get; private set; }

		public float MaxValue { get; private set; }

		public RegenerationBase Regeneration
		{
			get
			{
				if (_regeneration == null)
				{
					Debug.LogWarning("Regeneration is not assigned on " + base.gameObject.name + " game object.");
					return null;
				}
				return _regeneration;
			}
			private set
			{
				_regeneration = value;
			}
		}

		public event Action<float, float, bool> OnValueChanged;

		public event Action OnValueIncrease;

		public event Action OnValueDecrease;

		public event Action OnMaxValueIncrease;

		public event Action OnMaxValueDecrease;

		public event Action OnValueFull;

		public event Action OnValueEmpty;

		private void OnValidate()
		{
			if ((bool)VitalsConfig && !Application.isPlaying)
			{
				Reload();
			}
		}

		private void Awake()
		{
			if (!VitalsConfig)
			{
				Debug.LogError("Vitals Config is not assigned on " + base.gameObject.name + " game object.");
			}
		}

		protected virtual void Start()
		{
			if (!loadOnStart)
			{
				Reload();
			}
			else
			{
				VitalsSaveUtility.Load(this);
			}
		}

		private void OnEnable()
		{
			if ((bool)vitalsHook)
			{
				vitalsHook.Initialize(this);
			}
		}

		private void OnDisable()
		{
			if ((bool)vitalsHook)
			{
				vitalsHook.Unsubscribe(this);
			}
		}

		protected virtual void OnApplicationQuit()
		{
			if (saveOnQuit)
			{
				VitalsSaveUtility.Save(this);
			}
		}

		public void Reload()
		{
			SetMax(vitalsConfig.maxValue);
			Set(vitalsConfig.value);
		}

		public virtual void Increase(float amount, bool triggerEvents = true, bool isDrain = false)
		{
			if (!VitalsUtility.IsValidAmount(amount))
			{
				Debug.LogError("Amount can't be negative.");
				return;
			}
			if (Value >= MaxValue)
			{
				Debug.Log("Value is already full.");
				return;
			}
			Value += amount;
			Value = Mathf.Clamp(Value, 0f, MaxValue);
			this.OnValueChanged?.Invoke(Value, MaxValue, isDrain);
			if (triggerEvents)
			{
				this.OnValueIncrease?.Invoke();
			}
			if (Value >= MaxValue)
			{
				this.OnValueFull?.Invoke();
			}
		}

		public virtual void Decrease(float amount, bool triggerEvents = true, bool isDrain = false)
		{
			if (!VitalsUtility.IsValidAmount(amount))
			{
				Debug.LogError("Amount can't be negative.");
				return;
			}
			if (Value <= 0f)
			{
				Debug.Log("Value is already empty.");
				return;
			}
			Value -= amount;
			Value = Mathf.Clamp(Value, 0f, MaxValue);
			this.OnValueChanged?.Invoke(Value, MaxValue, isDrain);
			if (triggerEvents)
			{
				this.OnValueDecrease?.Invoke();
			}
			if (Value <= 0f)
			{
				this.OnValueEmpty?.Invoke();
			}
		}

		public virtual void IncreaseMax(float amount)
		{
			if (!VitalsUtility.IsValidAmount(amount))
			{
				Debug.LogError("Amount can't be negative.");
				return;
			}
			MaxValue += amount;
			this.OnValueChanged?.Invoke(Value, MaxValue, arg3: false);
			this.OnMaxValueIncrease?.Invoke();
		}

		public virtual void DecreaseMax(float amount)
		{
			if (!VitalsUtility.IsValidAmount(amount))
			{
				Debug.LogError("Amount can't be negative.");
				return;
			}
			MaxValue -= amount;
			if (MaxValue <= 0f)
			{
				Debug.LogError("Max Value can't be negative.");
				return;
			}
			this.OnValueChanged?.Invoke(Value, MaxValue, arg3: false);
			this.OnMaxValueDecrease?.Invoke();
			if (Value > MaxValue)
			{
				Value = MaxValue;
				this.OnValueFull?.Invoke();
			}
		}

		public virtual void Set(float value)
		{
			if (!VitalsUtility.IsValidAmount(value))
			{
				Debug.LogError("Set Value can't be negative.");
				return;
			}
			Value = Mathf.Clamp(value, 0f, MaxValue);
			this.OnValueChanged?.Invoke(Value, MaxValue, arg3: false);
			if (Value <= 0f)
			{
				this.OnValueEmpty?.Invoke();
			}
			else if (Value >= MaxValue)
			{
				this.OnValueFull?.Invoke();
			}
		}

		public virtual void SetMax(float value)
		{
			if (!VitalsUtility.IsValidAmount(value))
			{
				Debug.LogError("Set Value can't be negative.");
				return;
			}
			MaxValue = value;
			this.OnValueChanged?.Invoke(Value, MaxValue, arg3: false);
			if (Value > MaxValue)
			{
				Value = MaxValue;
				this.OnValueFull?.Invoke();
			}
		}

		public void SetRegeneration(RegenerationBase regeneration)
		{
			Regeneration = regeneration;
		}

		public void SetHook(VitalsHook hook)
		{
			vitalsHook = hook;
		}
	}
}
