using UnityEngine;

namespace Vitals
{
	public class CanvasLook : MonoBehaviour
	{
		private Transform _cameraTransform;

		private void Awake()
		{
			_cameraTransform = Camera.main.transform;
		}

		private void Update()
		{
			base.transform.LookAt(_cameraTransform);
		}
	}
}
