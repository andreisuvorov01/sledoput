using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPaused : MonoBehaviour
{
	public static bool GameIsPaused;

	public GameObject Pause_menu;

	public GameObject player;

	public GameObject Setting_panel;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	public void Resume()
	{
		Debug.Log("Resume");
		Pause_menu.SetActive(value: false);
		Time.timeScale = 1f;
		GameIsPaused = false;
		player.GetComponent<FirstPersonController>().enabled = true;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void Pause()
	{
		Debug.Log("Pause");
		Pause_menu.SetActive(value: true);
		Time.timeScale = 0f;
		GameIsPaused = true;
		player.GetComponent<FirstPersonController>().enabled = false;
		Cursor.lockState = CursorLockMode.None;
	}

	public void Continue()
	{
		Debug.Log("Load");
		Time.timeScale = 1f;
		SceneManager.LoadScene("Mainmenu");
	}

	public void setting()
	{
		Setting_panel.SetActive(value: true);
	}

	public void exit()
	{
		Debug.Log("Quit");
		Application.Quit();
	}
}
