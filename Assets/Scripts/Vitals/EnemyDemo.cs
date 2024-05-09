using System.Collections;
using UnityEngine;

namespace Vitals
{
	[RequireComponent(typeof(Health))]
	public class EnemyDemo : MonoBehaviour
	{
		private MeshRenderer _meshRenderer;

		public Color flashColor = Color.red;

		public float flashDuration = 0.2f;

		private Color _originalColor;

		private bool _isFlashing;

		public Health Health { get; private set; }

		private void Awake()
		{
			Health = GetComponent<Health>();
			_meshRenderer = GetComponent<MeshRenderer>();
			_originalColor = _meshRenderer.material.color;
		}

		public void Die()
		{
			Object.Destroy(base.gameObject);
		}

		public void Flash()
		{
			StartCoroutine(FlashRoutine());
		}

		private IEnumerator FlashRoutine()
		{
			if (!_isFlashing)
			{
				_isFlashing = true;
				Color startColor2 = _meshRenderer.material.color;
				float elapsedTime2 = 0f;
				while (elapsedTime2 < flashDuration)
				{
					float t = elapsedTime2 / flashDuration;
					_meshRenderer.material.color = Color.Lerp(startColor2, flashColor, t);
					elapsedTime2 += Time.deltaTime;
					yield return null;
				}
				startColor2 = _meshRenderer.material.color;
				elapsedTime2 = 0f;
				while (elapsedTime2 < flashDuration)
				{
					float t2 = elapsedTime2 / flashDuration;
					_meshRenderer.material.color = Color.Lerp(startColor2, _originalColor, t2);
					elapsedTime2 += Time.deltaTime;
					yield return null;
				}
				_meshRenderer.material.color = _originalColor;
				_isFlashing = false;
			}
		}
	}
}
