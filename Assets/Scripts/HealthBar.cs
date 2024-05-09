using UnityEngine;

public class HealthBar : MonoBehaviour
{
	public bool showBar;

	public float barWidth;

	public float barHeight;

	public int health;

	public int healthMax;

	private void Start()
	{
		showBar = false;
	}

	private void Update()
	{
	}

	private void OnGUI()
	{
		if (showBar)
		{
			GUI.Box(text: (health <= 0) ? "Dead" : (health + " / " + healthMax), position: new Rect((float)(Screen.width / 2) - barWidth / 2f, barHeight, barWidth, barHeight));
		}
	}
}
