using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class schotchik : MonoBehaviour
{
	public int Schotchik_value;

	public Text Vuvod;
    private void Start()
    {
		if (SceneManager.GetActiveScene().buildIndex == 2)
		{ 
			PlayerPrefs.SetInt("EnemyCount", 0);
        }
        Schotchik_value = PlayerPrefs.GetInt("EnemyCount");
        Vuvod.text = "Счёт: " + Schotchik_value;

    }

    private void Update()
	{
		PlayerPrefs.SetInt("EnemyCount", Schotchik_value);
		Vuvod.text = "Счёт: " + Schotchik_value;
	}
}
