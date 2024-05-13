using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPaused : MonoBehaviour
{
    public static bool GameIsPaused;

    public GameObject Pause_menu;
    public GameObject Setting_panel;

    public GameObject player;
    public GameObject gun;


    [SerializeField] private AudioListener audioListener;

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
        audioListener.enabled = true;
        Pause_menu.SetActive(value: false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        player.GetComponent<FirstPersonMovement>().enabled = true;
        player.GetComponent<Jump>().enabled = true;
        player.GetComponentInChildren<FirstPersonLook>().enabled = true;
        gun.GetComponent<Weapon>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        Debug.Log("Pause");
        audioListener.enabled = false;
        Pause_menu.SetActive(value: true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        player.GetComponent<FirstPersonMovement>().enabled = false;
        player.GetComponent<Jump>().enabled = false;
        gun.GetComponent<Weapon>().enabled = false;
        player.GetComponentInChildren<FirstPersonLook>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    /*public void Continue()
	{
		Debug.Log("Load");
		Time.timeScale = 1f;
        Pause_menu.SetActive(false);

	}*/
    public void MainMenu()
    {
        Debug.Log("Load");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Mainmenu");
    }

    public void setting()
    {
        Pause_menu.SetActive(false);
        Setting_panel.SetActive(true);
    }

    public void exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
