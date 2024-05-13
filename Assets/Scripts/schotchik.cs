using UnityEngine;
using UnityEngine.UI;

public class schotchik : MonoBehaviour
{
	public int Schotchik_value;

	public Text Vuvod;

	private void Update()
	{
		PlayerPrefs.SetInt("EnemyCount", Schotchik_value);
		Vuvod.text = "Счёт: " + Schotchik_value;
	}
}
