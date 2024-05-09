using UnityEngine;
using UnityEngine.UI;

public class VuvodScript : MonoBehaviour
{
	public int VuvodSchotchik;

	public Text MonsterKill;

	private void Start()
	{
		VuvodSchotchik = PlayerPrefs.GetInt("EnemyCount");
	}

	private void Update()
	{
		MonsterKill.text = "Монстров убито: " + VuvodSchotchik + "\nОчков набрано: " + VuvodSchotchik * 10;
	}
}
