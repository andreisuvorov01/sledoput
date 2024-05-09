using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int MaxHealth = 100;

	public int CurrentHealt;

	private void Start()
	{
		CurrentHealt = MaxHealth;
	}

	public void TakeDamage(int damage)
	{
		CurrentHealt -= damage;
		if (CurrentHealt <= 0)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
