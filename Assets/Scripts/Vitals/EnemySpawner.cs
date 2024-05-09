using UnityEngine;
using UnityEngine.InputSystem;

namespace Vitals
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField]
		private Transform enemyPrefab;

		private Camera _camera;

		public InputActionAsset actions;

		private InputAction _spawn;

		private void Awake()
		{
			_camera = Camera.main;
			_spawn = actions.FindActionMap("EnemyDemo").FindAction("Spawn");
			_spawn.performed += OnSpawn;
		}

		private void OnEnable()
		{
			_spawn.Enable();
		}

		private void OnSpawn(InputAction.CallbackContext obj)
		{
			Vector2 mousePosition = Mouse.current.position.ReadValue();
			Vector3 worldPosition = GetWorldPosition(mousePosition);
			worldPosition.y = 1f;
			Transform obj2 = Object.Instantiate(enemyPrefab, worldPosition, Quaternion.identity);
			Health component = obj2.GetComponent<Health>();
			VitalsUIBind component2 = obj2.GetComponent<VitalsUIBind>();
			VitalsUtility.Bind(component, component2);
		}

		private Vector3 GetWorldPosition(Vector2 mousePosition)
		{
			if (!Physics.Raycast(_camera.ScreenPointToRay(mousePosition), out var hitInfo))
			{
				return Vector3.zero;
			}
			return hitInfo.point;
		}
	}
}
