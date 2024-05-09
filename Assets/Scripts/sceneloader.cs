using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneloader : MonoBehaviour
{
	public GameObject Setting_panel;

	public void startButton()
	{
		SceneManager.LoadScene("Game");
	}

	public void OpenMenu()
	{
		SceneManager.LoadScene("Mainmenu");
	}

	public void CloseGame()
	{
		Application.Quit();
	}

	public void SettingMenu()
	{
		Setting_panel.SetActive(value: true);
	}

	public void StartSlider()
	{
		SceneManager.LoadScene("Assets/Scenes/slider.unity");
	}

	public void final()
	{
		SceneManager.LoadScene("SuccesGame");
	}
}
