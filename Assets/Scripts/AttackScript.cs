using UnityEngine;

public class AttackScript : MonoBehaviour
{
	private Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			anim.SetBool("attack", value: true);
		}
		else if (Input.GetButtonUp("Fire1"))
		{
			anim.SetBool("attack", value: false);
		}
	}
}
