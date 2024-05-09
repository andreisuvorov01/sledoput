using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    private int HP = 100;

	public Slider healthBar;

	private void Update()=>healthBar.value = HP;
	
	public void TakeDamage(int damageAmount)
	{
		HP -= damageAmount;
		if (healthBar.value <= 0f)
		{
			Cursor.lockState = CursorLockMode.None;
			SceneManager.LoadScene("EndGame");
			Debug.Log("LoadScene");
		}
	}
}
