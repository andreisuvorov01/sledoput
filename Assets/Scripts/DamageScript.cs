using UnityEngine;

public class DamageScript : MonoBehaviour
{
	public int damageAmount = 20;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			other.GetComponent<EnemyScript>().TakeDamage(damageAmount);
		}
	}
}
