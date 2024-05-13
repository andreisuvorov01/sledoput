using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneloader : MonoBehaviour
{
	public GameObject Setting_panel;
	public GameObject MainMenu_panel;

	public void LoadScene(int i)
	{
        SceneManager.LoadScene(i);
    }
   /* public void startButton()
	{
		SceneManager.LoadScene("GameLevel1");
	}

	public void OpenMenu()
	{
		SceneManager.LoadScene("Mainmenu");
	}*/

	public void CloseGame()
	{
		Application.Quit();
	}

	public void SettingMenu()
	{
		Setting_panel.SetActive(true);
        MainMenu_panel.SetActive(false);

    }

	/*public void StartSlider()
	{
		SceneManager.LoadScene("Assets/Scenes/slider.unity");
	}

	public void final()
	{
		SceneManager.LoadScene("SuccesGame");
	}*/
}
