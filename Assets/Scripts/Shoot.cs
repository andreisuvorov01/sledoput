using UnityEngine;

public class Shoot : MonoBehaviour
{
	public float bulletLife = 1f;

	private void Awake()
	{
		Object.Destroy(base.gameObject, bulletLife);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			Object.Destroy(base.gameObject);
		}
	}
}
