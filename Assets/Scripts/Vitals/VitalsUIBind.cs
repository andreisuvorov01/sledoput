using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Vitals
{
	[DefaultExecutionOrder(10)]
	public class VitalsUIBind : MonoBehaviour
	{
		[SerializeField]
		private VitalsHook vitalsHook;

		[SerializeField]
		private UIType uiType;

		[SerializeField]
		private Image fillImage;

		[SerializeField]
		private Slider slider;

		[SerializeField]
		private bool showTextFields;

		[SerializeField]
		private Text valueText;

		[SerializeField]
		private Text maxValueText;

		[Header("[Optional]")]
		[SerializeField]
		[Tooltip("If UI binding is setup through code, enabling this will disable the Debug.Log that checks for binding on Awake")]
		private bool boundThroughCode;

		[Header("Animation")]
		[SerializeField]
		private bool animateChanges;

		[SerializeField]
		private float animationDuration = 0.5f;

		[SerializeField]
		private AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

		private Action<float, float, bool> _updateUI;

		private Coroutine _animateCoroutine;

		public UIType UIType => uiType;

		public bool ShowTextFields => showTextFields;

		public bool AnimateChanges => animateChanges;

		private void Awake()
		{
			if (!vitalsHook && !boundThroughCode)
			{
				Debug.Log("Vitals Hook is not assigned on " + base.gameObject.name + " game object.");
			}
		}

		private void OnEnable()
		{
			switch (uiType)
			{
			case UIType.Image:
				_updateUI = UpdateImage;
				if (!fillImage)
				{
					Debug.LogError("Fill Image is not assigned on " + base.gameObject.name + " game object.");
					return;
				}
				break;
			case UIType.Slider:
				_updateUI = UpdateSlider;
				if (!slider)
				{
					Debug.LogError("Slider is not assigned on " + base.gameObject.name + " game object.");
					return;
				}
				slider.maxValue = 1f;
				break;
			}
			if ((bool)vitalsHook)
			{
				if (!vitalsHook.Initialized)
				{
					Debug.Log("Vitals Hook is not initialized on " + base.gameObject.name + " game object.\nPlease make sure that " + vitalsHook.name + " is assigned to a Health/Stamina component.");
				}
				else
				{
					vitalsHook.OnValueChanged += _updateUI;
				}
			}
		}

		private void OnDisable()
		{
			if ((bool)vitalsHook)
			{
				vitalsHook.OnValueChanged -= _updateUI;
			}
		}

		private void UpdateImage(float value, float maxvalue, bool isDrain)
		{
			if (_animateCoroutine != null)
			{
				StopCoroutine(_animateCoroutine);
			}
			if (animateChanges && !isDrain)
			{
				_animateCoroutine = StartCoroutine(AnimateImage(value, maxvalue));
				return;
			}
			fillImage.fillAmount = VitalsUtility.GetPercentage(vitalsHook);
			if (showTextFields)
			{
				UpdateText();
			}
		}

		private void UpdateSlider(float value, float maxvalue, bool isDrain)
		{
			if (_animateCoroutine != null)
			{
				StopCoroutine(_animateCoroutine);
			}
			if (animateChanges && !isDrain)
			{
				_animateCoroutine = StartCoroutine(AnimateSlider(value, maxvalue));
				return;
			}
			slider.value = VitalsUtility.GetPercentage(vitalsHook);
			if (showTextFields)
			{
				UpdateText();
			}
		}

		private void UpdateText()
		{
			if ((bool)valueText)
			{
				valueText.text = vitalsHook.Value.ToString("F0");
			}
			if ((bool)maxValueText)
			{
				maxValueText.text = vitalsHook.MaxValue.ToString("F0");
			}
		}

		private IEnumerator AnimateImage(float value, float maxValue)
		{
			float startValue = fillImage.fillAmount;
			float elapsedTime = 0f;
			float targetValue = VitalsUtility.GetPercentage(value, maxValue);
			while (elapsedTime < animationDuration)
			{
				elapsedTime += Time.deltaTime;
				float time = Mathf.Clamp01(elapsedTime / animationDuration);
				float fillAmount = Mathf.Lerp(startValue, targetValue, animationCurve.Evaluate(time));
				fillImage.fillAmount = fillAmount;
				yield return null;
			}
			fillImage.fillAmount = targetValue;
			if (showTextFields)
			{
				UpdateText();
			}
			_animateCoroutine = null;
		}

		private IEnumerator AnimateSlider(float value, float maxValue)
		{
			float startValue = slider.value;
			float elapsedTime = 0f;
			float targetValue = VitalsUtility.GetPercentage(value, maxValue);
			while (elapsedTime < animationDuration)
			{
				elapsedTime += Time.deltaTime;
				float time = Mathf.Clamp01(elapsedTime / animationDuration);
				float value2 = Mathf.Lerp(startValue, targetValue, animationCurve.Evaluate(time));
				slider.value = value2;
				yield return null;
			}
			slider.value = targetValue;
			if (showTextFields)
			{
				UpdateText();
			}
			_animateCoroutine = null;
		}

		public void Initialize(VitalsHook hook)
		{
			vitalsHook = hook;
			vitalsHook.OnValueChanged += _updateUI;
		}
	}
}
