using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sattings : MonoBehaviour
{
	public Dropdown resolusionDropdown;

	public Dropdown qualityDropdown;

	public GameObject Setting_panel;

	public Slider soundVolium;

	private Resolution[] resolutions;

	private bool restartSettings;

	private void Start()
	{
		soundVolium.value = PlayerPrefs.GetFloat("playerListenerSettings");
		resolusionDropdown.ClearOptions();
		List<string> list = new List<string>();
		resolutions = Screen.resolutions;
		int currentResalutionsIndex = 0;
		for (int i = 0; i < resolutions.Length; i++)
		{
			string item = resolutions[i].width + "x" + resolutions[i].height;
			list.Add(item);
			if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
			{
				currentResalutionsIndex = i;
			}
		}
		resolusionDropdown.value = PlayerPrefs.GetInt("resolusionSetingsValue");
		resolusionDropdown.AddOptions(list);
		resolusionDropdown.RefreshShownValue();
		LoadSettings(currentResalutionsIndex);
	}

	private void Update()
	{
		if (restartSettings)
		{
			restartSettings = false;
		}
		if (Input.GetKeyDown(KeyCode.P))
		{
			PlayerPrefs.DeleteAll();
		}
	}

	public void SetResolution(int resalutionsIndex)
	{
		Resolution resolution = resolutions[resalutionsIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetSensitivitySettings()
	{
	}

	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void SoundValue(float soundVoliumFloat)
	{
		PlayerPrefs.SetFloat("playerListenerSettings", soundVoliumFloat);
		AudioListener.volume = soundVoliumFloat;
	}

	public void SaveSettings()
	{
		PlayerPrefs.SetInt("QualitySetingsValue", qualityDropdown.value);
		PlayerPrefs.SetInt("resolusionSetingsValue", resolusionDropdown.value);
		restartSettings = true;
		Setting_panel.SetActive(value: false);
	}

	public void LoadSettings(int currentResalutionsIndex)
	{
		if (PlayerPrefs.HasKey("QualitySetingsValue"))
		{
			qualityDropdown.value = PlayerPrefs.GetInt("QualitySetingsValue");
		}
		else
		{
			qualityDropdown.value = 3;
		}
		if (PlayerPrefs.HasKey("resolusionSetingsValue"))
		{
			resolusionDropdown.value = PlayerPrefs.GetInt("resolusionSetingsValue");
		}
		else
		{
			resolusionDropdown.value = currentResalutionsIndex;
		}
	}
}
