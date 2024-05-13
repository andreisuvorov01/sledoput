using UnityEngine;

public class DamageGG : MonoBehaviour
{
	public int damageAmount;
    public FollowScript follow;
    private void Start()
    {
        follow = GetComponent<FollowScript>();
    }
    private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.GetComponent<health>().TakeDamage(damageAmount);
            follow.animatok.SetInteger("move", 2);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            follow.animatok.SetInteger("move", 0);
        }
    }
}
