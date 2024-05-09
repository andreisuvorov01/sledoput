using UnityEngine;

public class MenuSpravka : MonoBehaviour
{
	public static bool SpravkaIsPaused;

	public GameObject Spravka_menu;

	public void Open()
	{
		if (!SpravkaIsPaused)
		{
			Spravka_menu.SetActive(value: true);
			SpravkaIsPaused = true;
			Debug.Log("Справка открыта");
		}
		else
		{
			Spravka_menu.SetActive(value: false);
			SpravkaIsPaused = false;
			Debug.Log("Справка закрыта");
		}
	}
}
