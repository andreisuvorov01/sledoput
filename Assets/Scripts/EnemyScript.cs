using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
	public schotchik schotchik_script;

	public int HP = 100;

	public Animator animatok;

	public Slider healthBar;

	private void Update()
	{
		healthBar.value = HP;
	}

	public void TakeDamage(int damageAmount)
	{
		HP -= damageAmount;
		if (HP <= 0)
		{
			schotchik_script.Schotchik_value++;
			GetComponent<Collider>().enabled = false;
			healthBar.gameObject.SetActive(value: false);
			Object.Destroy(base.transform.gameObject);
		}
	}
}
