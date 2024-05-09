using UnityEngine;
using UnityEngine.SceneManagement;

public class finalBoss : MonoBehaviour
{
	public GameObject slime;

	private void Start()
	{
	}

	private void Update()
	{
		if (!slime)
		{
			Cursor.lockState = CursorLockMode.None;
			SceneManager.LoadScene("sliderFinal");
		}
	}
}
