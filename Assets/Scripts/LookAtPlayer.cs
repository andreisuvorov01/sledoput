using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
	public Transform cam;

    private void Start()
    {
        cam = GameObject.Find("PlayerCamera").transform;
    }
    private void LateUpdate()
	{
		transform.LookAt(cam);
	}
}
