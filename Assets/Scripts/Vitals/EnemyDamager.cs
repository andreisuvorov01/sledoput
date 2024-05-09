using UnityEngine;
using UnityEngine.InputSystem;

namespace Vitals
{
	public class EnemyDamager : MonoBehaviour
	{
		public float damage = 10f;

		public InputActionAsset actions;

		private InputAction _damageAction;

		private void Awake()
		{
			_damageAction = actions.FindActionMap("EnemyDemo").FindAction("Damage");
			_damageAction.performed += OnDamageAction;
		}

		private void OnDamageAction(InputAction.CallbackContext obj)
		{
			Vector2 vector = Mouse.current.position.ReadValue();
			if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), out var hitInfo) && hitInfo.transform.TryGetComponent<EnemyDemo>(out var component))
			{
				component.Health.Decrease(damage);
			}
		}

		private void OnEnable()
		{
			_damageAction.Enable();
		}
	}
}
